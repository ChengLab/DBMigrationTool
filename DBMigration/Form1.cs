using CodeGen;
using CodeGen.Models;
using CodeGen.Utility;
using DBMigration.Migrations;
using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DBMigration
{
    public partial class Form1 : Form
    {

        #region Field
        public Process process = null;
        private static readonly DatabaseServices DatabaseServices = new DatabaseServices();

        #endregion Field

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBoxGenCode.SelectedIndex = 0;
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void buttonCodeGen_Click(object sender, EventArgs e)
        {

            bool isCreate = true;
            switch (this.comboBoxGenCode.SelectedIndex)
            {
                case 0://0生成创建表代码
                    GenMigrationCode(isCreate);
                    break;
                case 1://1生成删除表代码
                    isCreate = false;
                    GenMigrationCode(isCreate);
                    break;
                default:
                    MessageBox.Show("unkonow");
                    break;
            }
        }
        #region method

        private void GenMigrationCode(bool isCreate)
        {
            var filePath = isCreate ? ConfigHelper.GetCreateTableFilePath() : ConfigHelper.GetDeleteTableFilePath();
            var title = isCreate ? "创建表" : "删除表";
            ShowLog(this.textBoxShowLog, string.Format("正在生成{0}的代码到{1}，请稍后...", title, filePath));

            var list =
                DatabaseServices.GetTables().Where(i => !i.Name.Equals("__MigrationHistory")).OrderBy(i => i.Name).Select(i => new StatusItem<TableModel> { Model = i, Status = false }).ToArray();

            var tables = new List<TableModel>();

            Action<StatusItem<TableModel>> handlerTable = item =>
            {
                //已经处理过则不进行处理。
                if (item.Status)
                    return;
                item.Status = true;
                tables.Add(item.Model);
            };

            Action<StatusItem<TableModel>> handler = null;

            handler = item =>
            {
                var table = item.Model;

                //确保外键表被优先处理。
                if (table.ForeignKeys != null && table.ForeignKeys.Any())
                {
                    //未被处理的表名称，并且不是自引用（表本身）。
                    var noTableNames = table.ForeignKeys.Where(i => i.ReferencedTable != table.Name && tables.All(z => i.Name != z.Name)).Select(i => i.ReferencedTable).ToArray();
                    //未被处理的表。
                    var noTables = list.Where(i => noTableNames.Contains(i.Model.Name)).ToArray();
                    foreach (var statusItem in noTables)
                    {
                        handler(statusItem);
                    }
                }

                //处理当前表。
                handlerTable(item);
            };

            foreach (var item in list.Where(item => !item.Status))
            {
                handler(item);
            }

            //输出。
            Output(tables, isCreate);
        }
        private void Output(IEnumerable<TableModel> tables, bool isCreate)
        {
            var file = new FileInfo(isCreate ? ConfigHelper.GetCreateTableFilePath() : ConfigHelper.GetDeleteTableFilePath());
            if (file.Directory != null && !file.Directory.Exists)
                file.Directory.Create();

            var builder = new StringBuilder();
            foreach (var tableModel in tables)
            {
                builder.AppendLine(isCreate ? tableModel.ToCreateCode() : tableModel.ToDeleteCode(false));
            }
            File.WriteAllText(file.FullName, builder.ToString());
            ShowLog(this.textBoxShowLog, (isCreate ? "创建表" : "删除表") + "的代码生成成功...");


        }

        private void ShowLog(System.Windows.Forms.TextBox textBox, string info)
        {
            textBox.AppendText(info);
            textBox.AppendText(Environment.NewLine);
            textBox.ScrollToCaret();
        }
        #endregion

        private void textBoxShowLog_TextChanged(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// DB up
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            if (this.textBoxInputCmd.Text != null && !textBoxInputCmd.Text.Trim().Equals(""))
            {

                if (this.textBoxProjectFile.Text != null && !textBoxProjectFile.Text.Trim().Equals(""))
                {
                    CallCmd(this.textBoxInputCmd.Text.Replace("\r", "").Replace("\n", ""));
                }
                else
                {
                    MessageBox.Show("请输入你的项目路径");
                }
            }
            else
            {
                MessageBox.Show("输入不用为空；请参考软件说明--》命令示例");
            }
        }
        /// <summary>
        /// down
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {

        }


        public void CallCmd(string command)
        {
            Control.CheckForIllegalCrossThreadCalls = false; //避免线程间操作无效: 从不是创建控件“xxxx”的线程访问它。

            if (command != null && !command.Equals(""))
            {
                //创建进程对象 
                process = new Process();
                //设定需要执行的命令 
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.FileName = "cmd.exe";
                startInfo.WorkingDirectory = this.textBoxProjectFile.Text;
                //不使用系统外壳程序启动
                startInfo.UseShellExecute = false;
                //不重定向输入 
                startInfo.RedirectStandardInput = true;
                //重定向输出 
                startInfo.RedirectStandardOutput = true;
                //不创建窗口 
                startInfo.CreateNoWindow = true;
                process.OutputDataReceived += new DataReceivedEventHandler(OutputHandler);
                process.StartInfo = startInfo;
                try
                {
                    //开始进程
                    if (process.Start())
                    {
                        process.StandardInput.WriteLine(command);
                        process.BeginOutputReadLine();
                    }
                }
                catch (Exception ex)
                {

                }


            }

        }

        private void OutputHandler(object sendingProcess, DataReceivedEventArgs outLine)
        {
            if (!String.IsNullOrEmpty(outLine.Data))
            {
                StringBuilder sb = new StringBuilder(this.textBoxMlog.Text);
                this.textBoxMlog.Text = sb.AppendLine(outLine.Data).ToString();
                this.textBoxMlog.SelectionStart = this.textBoxMlog.Text.Length;
                this.textBoxMlog.ScrollToCaret();
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (process != null)
                process.Close();
        }
    }

    #region Help Class

    public class StatusItem<T>
    {
        public T Model { get; set; }

        public bool Status { get; set; }
    }

    #endregion Help Class
}
