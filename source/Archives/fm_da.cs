using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DrSofts.Servers;

namespace DrSofts.Clients.BodyChecks.Archives
{
    public partial class fm_da : Form
    {
        private DataSet _un;

        public fm_da()
        {
            InitializeComponent();
        }        
         
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                //return ;
                CheckDb mydb = new CheckDb();
                _un = mydb.retrieve_un();


                _un.Tables["infoCompany"].PrimaryKey = new DataColumn[]
                {
                     _un.Tables["infoCompany"].Columns["iCompanyNo"]
                };


                DataView dv = new DataView(_un.Tables["infoCompany"]);
                DataView dv_1 = new DataView(_un.Tables["infoCompanyRecord"]);

                gridControl1.DataSource = dv;

                _un.Relations.Add("Level1", _un.Tables["infoCompanyRecord"].Columns["iRecordNo"],
                    _un.Tables["infoCompanyGroup"].Columns["iRecordNo"]);
                //
                dv_1 = new DataView(_un.Tables["infoCompanyGroup"]);
                if (gridView1.RowCount > 1) 
                {
                    dv_1.RowFilter = "iCompanyNo= " + gridView1.GetDataRow(1)["iCompanyNo"].ToString();
                }

                //单位编号
                textBox1.DataBindings.Clear();
                textBox1.DataBindings.Add("Text", dv, "cCode");

                //单位名称
                textBox2.DataBindings.Clear();
                textBox2.DataBindings.Add("Text", dv, "cName");
                //地址
                textBox3.DataBindings.Clear();
                textBox3.DataBindings.Add("Text", dv, "cAdd");

                //账号
                textBox4.DataBindings.Clear();
                textBox4.DataBindings.Add("Text", dv, "cBankCode");

                //单位类型
                comboBox1.DataBindings.Clear();
                DataView dv1 = Dbs.Data.GetDataView("infodictCompanyType");
                comboBox1.DataSource = dv1;
                comboBox1.DisplayMember = "cName";
                comboBox1.ValueMember = "iNo";

                //联系人
                textBox6.DataBindings.Clear();
                textBox6.DataBindings.Add("Text", dv, "cLinkMan");

                //开户行
                textBox8.DataBindings.Clear();
                textBox8.DataBindings.Add("Text", dv, "cBank");

                //备注
                textBox9.DataBindings.Clear();
                textBox9.DataBindings.Add("Text", dv, "cMemo");

                //拼音吗
                textBox14.DataBindings.Clear();
                textBox14.DataBindings.Add("Text", dv, "cSpell");

                //登记人 cname,no_staff
                comboBox2.DataBindings.Clear();
                DataView dv2 = Dbs.Data.GetDataView("pubdictstaff");
                comboBox2.DataSource = dv2;
                comboBox2.DisplayMember = "cName";
                comboBox2.ValueMember = "no_staff";
                repositoryItemGridLookUpEdit1.DataSource = dv2;
                repositoryItemGridLookUpEdit1.DisplayMember = "cName";
                repositoryItemGridLookUpEdit1.ValueMember = "no_staff";


                //电话
                maskedTextBox1.DataBindings.Clear();
                maskedTextBox1.DataBindings.Add("Text", dv, "cPhone");

                //邮编
                maskedTextBox2.DataBindings.Clear();
                maskedTextBox2.DataBindings.Add("Text", dv, "cZip");

                //登记日期
                dateTimePicker1.DataBindings.Clear();
                dateTimePicker1.DataBindings.Add("Text", dv, "dDate");

                //体检次数
                checkEdit1.DataBindings.Clear();
                checkEdit1.DataBindings.Add("EditValue", dv, "bIsNotShow");

                
                textBox1.Focus();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }                      
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (DataRow dr in _un.Tables["infoCompany"].Rows)
                {
                    dr.EndEdit();
                }
                if (_un.Tables["infoCompany"].GetChanges() == null)
                {
                    MessageBox.Show("");
                    return;
                }
                DataSet ds = new DataSet();
                ds.Tables.Add(_un.Tables["infoCompany"].GetChanges());
                CheckDb mydb = new CheckDb();
                mydb.SaveData(ds);
                mydb.Dispose();
                CommonForms.CommonFun.ShowTipInforWithMoveForm("保存成功!");
                _un.AcceptChanges();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }
       
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                this.DeleteArchives();
                
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }
        //删除数据
        private void DeleteArchives()
        {
            try
            {
                CheckDb mydb = new CheckDb();
                if (_un == null || _un.Tables.Count < 1 || _un.Tables[0].Rows.Count < 1)
                {
                    return;
                }
                DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
                if (dr == null)
                {
                    CommonForms.CommonFun.ShowTipInforWithMoveForm("请选择要删除的单位!");
                    return;
                }                
                DialogResult result = (MessageBox.Show("是否确定删除该行数据并保存?", "操作确认",
                    MessageBoxButtons.OKCancel, MessageBoxIcon.Question));
                if (result == DialogResult.OK)
                {
                    //判断该单位有无体检人员信息
                    string sSql = @"select iEnrolNo from infoEnrolMain where iCompanyNo=" + dr["iCompanyNo"];
                    DataSet ds = mydb.ExecuteSelect(sSql);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        CommonForms.CommonFun.ShowTipInforWithMoveForm("该单位已有体检人员信息,不允许删除!");
                        return;
                    }
                    dr.Delete();
                    mydb.SaveData(_un);
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }


        private void fm_da_keydown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SendKeys.Send("{Tab}");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                DataRow dr = _un.Tables["infoCompany"].NewRow();
                dr["iCompanyNo"] = Commons.IdCenter.GetIdWithTableName("infoCompany");
                dr["cCode"] = "";
                dr["cName"] = "";
                dr["cSpell"] = "";
                dr["cAdd"] = "";
                dr["cPhone"] = "";
                dr["cZip"] = "";
                dr["cLinkMan"] = "";
                dr["iTypeNo"] = 0;
                dr["cBank"] = "";
                dr["cBankCode"] = "";
                dr["dDate"] = DateTime.Today;
                dr["iOpNo"] = Dbs.Data.OPID;
                dr["cMemo"] = "";
                dr["iCheckOrder"] = 1;
                _un.Tables["infoCompany"].Rows.Add(dr);
                textBox1.Focus();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        private void fm_da_closing(object sender, FormClosingEventArgs e)
        {
            try
            {
                CheckDb mydb = new CheckDb();
                foreach (DataRow dr in _un.Tables["infoCompany"].Rows)
                {
                    dr.EndEdit();
                }
                if (_un.Tables["infoCompany"].GetChanges() == null)
                {
                    return;
                }
                DialogResult result = (MessageBox.Show("是否确定修改的数据保存?", "操作确认",
                    MessageBoxButtons.OKCancel, MessageBoxIcon.Question));
                if (result == DialogResult.OK)
                {
                    mydb.SaveData(_un);
                }
                else
                    this.Close();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        private void textbox2_changed(object sender, EventArgs e)
        {
            textBox14.Text = Commons.Common.GetChineseSpell(textBox2.Text);
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                DataView dv_1 = new DataView(_un.Tables["infoCompanyRecord"]);
                dv_1.RowFilter = "iCompanyNo= " + gridView1.GetDataRow(e.FocusedRowHandle)["iCompanyNo"].ToString();
                gridControl3.DataSource = dv_1;
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }
    }
}