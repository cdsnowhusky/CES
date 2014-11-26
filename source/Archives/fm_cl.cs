using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DrSofts.Servers;
using System.IO;

namespace DrSofts.Clients.BodyChecks.Archives
{
    public partial class fm_cl : Form
    {

        private DataView _dv;
        private DataView dv;
        private CheckDb mydb;
        DataView infoCompanyGroup;
        public fm_cl()
        {
            InitializeComponent();
        }
        private DataSet _archives;

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.init();
            this.initimage();
            lookUpEdit7.Focus();
        }

        private void init()
        {
            try
            {
                mydb = new CheckDb();
                _archives = mydb.retrieve_un();

                _archives.Tables["infoManArchives"].PrimaryKey = new DataColumn[]
                {
                     _archives.Tables["infoManArchives"].Columns["iArchivesNo"]
                };
                _dv = new DataView(_archives.Tables["infoEnrolMain"]);
                dv = new DataView(_archives.Tables["infoManArchives"]);

                gridControl1.DataSource = dv;


                if (gridView1.RowCount > 0)
                {
                    _dv.RowFilter = "iArchivesNo = " + gridView1.GetDataRow(1)["iArchivesNo"].ToString();
                }
                gridControl2.DataSource = _dv;


                //分组情况
                infoCompanyGroup = Dbs.Data.GetDataView("infoCompanyGroup");
                lookUpEdit9.DataBindings.Clear();
                lookUpEdit9.Properties.DataSource = infoCompanyGroup;
                lookUpEdit9.Properties.ValueMember = "iGroupNo";
                lookUpEdit9.Properties.DisplayMember = "cName";
                lookUpEdit9.DataBindings.Add("EditValue", dv, "iGroupNo");
                //体检单位lookUpEdit7
                DataView DictCompay = Dbs.Data.GetDataView("infoCompany");
                lookUpEdit7.DataBindings.Clear();
                lookUpEdit7.Properties.DataSource = DictCompay;
                lookUpEdit7.Properties.ValueMember = "iCompanyNo";
                lookUpEdit7.Properties.DisplayMember = "cName";
                lookUpEdit7.DataBindings.Add("EditValue", dv, "iCompanyNo");

                //职称
                DataView DictTitle = Dbs.Data.GetDataView("DictTitle");
                lookUpEdit3.DataBindings.Clear();
                lookUpEdit3.Properties.DataSource = DictTitle;
                lookUpEdit3.Properties.ValueMember = "No_Title";
                lookUpEdit3.Properties.DisplayMember = "TitleName";
                lookUpEdit3.DataBindings.Add("EditValue", dv, "NO_Title");


                //证件号码
                textBox4.DataBindings.Clear();
                textBox4.DataBindings.Add("Text", dv, "cIdCard");

                //登记号码
                textBox22.DataBindings.Clear();
                textBox22.DataBindings.Add("Text", dv, "cCheckCode");



                //备注
                textBox13.DataBindings.Clear();
                textBox13.DataBindings.Add("Text", dv, "cMemo");

                //姓名
                textBox2.DataBindings.Clear();
                textBox2.DataBindings.Add("Text", dv, "cName");

                //登记人 cname,no_staff
                DataView staff = Dbs.Data.GetDataView("pubdictstaff");
                lookUpEdit6.DataBindings.Clear();
                lookUpEdit6.Properties.DataSource = staff;
                lookUpEdit6.Properties.ValueMember = "No_Staff";
                lookUpEdit6.Properties.DisplayMember = "cName";
                //lookUpEdit6.DataBindings.Clear();
                lookUpEdit6.DataBindings.Add("EditValue", dv, "iOpNo");


                //拼音
                maskedTextBox1.DataBindings.Clear();
                maskedTextBox1.DataBindings.Add("Text", dv, "cSpell");

                //性别
                DataView sex = Dbs.Data.GetDataView("DictSex");
                lookUpEdit1.DataBindings.Clear();
                lookUpEdit1.Properties.DataSource = sex;
                lookUpEdit1.Properties.ValueMember = "No_Sex";
                lookUpEdit1.Properties.DisplayMember = "SexName";
                lookUpEdit1.DataBindings.Add("EditValue", dv, "iSex");


                //体检人员类别
                DataView PatientType = Dbs.Data.GetDataView("DictPatientType");
                lookUpEdit4.DataBindings.Clear();
                lookUpEdit4.Properties.DataSource = PatientType;
                lookUpEdit4.Properties.ValueMember = "No_PatientType";
                lookUpEdit4.Properties.DisplayMember = "PatientTypeName";
                lookUpEdit4.DataBindings.Add("EditValue", dv, "iPersonType");
                //联系地址
                textBox8.DataBindings.Clear();
                textBox8.DataBindings.Add("Text", dv, "cAdd");

                //联系电话
                maskedTextBox4.DataBindings.Clear();
                maskedTextBox4.DataBindings.Add("Text", dv, "cPhone");

                //电子邮箱
                textBox11.DataBindings.Clear();
                textBox11.DataBindings.Add("Text", dv, "cEMail");

                //职业
                DataView DictJob = Dbs.Data.GetDataView("DictJob");
                lookUpEdit2.DataBindings.Clear();
                lookUpEdit2.Properties.DataSource = DictJob;
                lookUpEdit2.Properties.ValueMember = "No_Job";
                lookUpEdit2.Properties.DisplayMember = "JobName";
                lookUpEdit2.DataBindings.Add("EditValue", dv, "iWorkNo");

                //民族
                DataView DictNation = Dbs.Data.GetDataView("DictNation");
                lookUpEdit8.DataBindings.Clear();
                lookUpEdit8.Properties.DataSource = DictNation;
                lookUpEdit8.Properties.ValueMember = "No_Nation";
                lookUpEdit8.Properties.DisplayMember = "NationName";
                lookUpEdit8.DataBindings.Add("EditValue", dv, "iNationNo");


                //是否已婚
                checkBox1.DataBindings.Clear();
                checkBox1.DataBindings.Add("Text", dv, "bIfWed");

                //登记时间
                maskedTextBox1.DataBindings.Clear();
                maskedTextBox1.DataBindings.Add("Text", dv, "dDate");

                //备注
                textBox13.DataBindings.Clear();
                textBox13.DataBindings.Add("Text", dv, "cMemo");

                //年龄单位
                DataView hryearsunit = Dbs.Data.GetDataView("hryearsunit");
                lookUpEdit5.DataBindings.Clear();
                lookUpEdit5.Properties.DataSource = hryearsunit;
                lookUpEdit5.Properties.ValueMember = "iYearsUnit";
                lookUpEdit5.Properties.DisplayMember = "cName";
                lookUpEdit5.DataBindings.Add("EditValue", dv, "iYearUnit");

                //年龄
                textBox4.DataBindings.Clear();
                textBox4.DataBindings.Add("Text", dv, "iYearsOld");

                //体检次数
                maskedTextBox3.DataBindings.Clear();
                maskedTextBox3.DataBindings.Add("Text", dv, "iCheckOrder");

                //出生日期
                maskedTextBox3.DataBindings.Clear();
                maskedTextBox3.DataBindings.Add("Text", dv, "dBirthdayDate");

                //是否儿童
                checkBox2.DataBindings.Clear();
                checkBox2.DataBindings.Add("Text", dv, "bYoungFlag");

            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }


        private void initimage()
        {

            byte[] img = Commons.Common.ToBytes(dv[0]["gImage"]);
            if (img.Length > 0)
            {
                System.IO.MemoryStream stream = new System.IO.MemoryStream(img, true);
                stream.Write(img, 0, img.Length);
                pictureBox1.Image = new Bitmap(stream);
                stream.Close();
            }
        }


        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                //_archives.Tables["infoManArchives"].Clear();
                DataRow dr = _archives.Tables["infoManArchives"].NewRow();
                dr["iArchivesNo"] = Commons.IdCenter.GetIdWithTableName("infoManArchives");
                dr["iGroupNo"] = 0;
                dr["iCompanyNo"] = 1;
                dr["NO_Title"] = 0;
                dr["NO_Marriage"] = 0;
                dr["cIdCard"] = "";
                dr["cCheckCode"] = "";
                dr["cName"] = "";
                dr["cSpell"] = "";
                dr["iSex"] = 0;

                dr["iPersonType"] = 0;
                dr["cAdd"] = "";
                dr["cPhone"] = "";
                dr["cEMail"] = "";
                dr["iWorkNo"] = 0;
                dr["iNationNo"] = 0;
                dr["bIfWed"] = 0;
                dr["dDate"] = DateTime.Today;
                dr["iOpNo"] = Dbs.Data.OPID;

                dr["cMemo"] = "";
                dr["iYearUnit"] = 0;
                dr["iYearsOld"] = 0;
                dr["iCheckOrder"] = 0;
                dr["dBirthdayDate"] = DateTime.Today;
                dr["bYoungFlag"] = 0;
                _archives.Tables["infoManArchives"].Rows.Add(dr);
                //gridControl1.Focus();
                if (gridView1.RowCount > 0)
                {
                    gridView1.FocusedRowHandle = gridView1.RowCount - 1;
                }
                lookUpEdit7.Focus();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.deleterow();
        }

        private void deleterow()
        {
            try
            {
                if (_archives == null || _archives.Tables.Count < 1 || _archives.Tables[0].Rows.Count < 1)
                {
                    return;
                }
                int position = gridView1.GetSelectedRows()[0];
                DialogResult result = (MessageBox.Show("是否删除该人员信息?", "提示",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question));
                if (result == DialogResult.Yes)
                {
                    gridView1.GetDataRow(gridView1.FocusedRowHandle).Delete();
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        private void savarow()
        {
            try
            {

                foreach (DataRow dr in _archives.Tables["infoManArchives"].Rows)
                {
                    dr.EndEdit();
                }
                if (_archives.Tables["infoManArchives"].GetChanges() == null)
                {
                    CommonForms.CommonFun.ShowTipInforWithMoveForm("数据未更新,无需保存!");
                    return;
                }
                DataSet ds = new DataSet();
                ds.Tables.Add(_archives.Tables["infoManArchives"].GetChanges());                
                mydb.SaveData(ds);
                mydb.Dispose();
                CommonForms.CommonFun.ShowTipInforWithMoveForm("保存成功!");
                _archives.AcceptChanges();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }



        private void cl_close(object sender, FormClosedEventArgs e)
        {
            try
            {
                foreach (DataRow dr in _archives.Tables["infoManArchives"].Rows)
                {
                    dr.EndEdit();
                }
                if (_archives.Tables["infoManArchives"].GetChanges() == null)
                {
                    return;
                }
                DialogResult result = (MessageBox.Show("是否确定修改的数据保存?", "操作确认",
                    MessageBoxButtons.OKCancel, MessageBoxIcon.Question));
                if (result == DialogResult.OK)
                {
                    mydb.SaveData(_archives);
                }
                else
                    return;
                //this.Close();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.savarow();
        }

        private void cl_key(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SendKeys.Send("{Tab}");
            }
        }

        private void changed(object sender, EventArgs e)
        {
            textBox20.Text = Commons.Common.GetChineseSpell(textBox2.Text);
        }

        private void editvaluechanged(object sender, EventArgs e)
        {
            try
            {
                int iCompanyNo = 0;
                if (lookUpEdit7.EditValue.ToString() != "")
                {
                    iCompanyNo = int.Parse(lookUpEdit7.EditValue.ToString());
                    infoCompanyGroup.RowFilter = "iCompanyNo = " + iCompanyNo.ToString();
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                if (gridView1.RowCount > 0)
                {
                    _dv.RowFilter = "iArchivesNo = " + gridView1.GetDataRow(e.FocusedRowHandle)["iArchivesNo"].ToString();

                    pictureBox1.Image = null;
                    byte[] img = Commons.Common.ToBytes(dv[e.FocusedRowHandle]["gImage"]);
                    if (img.Length > 0)
                    {
                        System.IO.MemoryStream stream = new System.IO.MemoryStream(img, true);
                        stream.Write(img, 0, img.Length);
                        pictureBox1.Image = new Bitmap(stream);
                        stream.Close();
                    }
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            openFileDialog1.RestoreDirectory = true;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {


                string fileName = openFileDialog1.FileName;


                FileStream fs = new FileStream(fileName, FileMode.Open);
                BinaryReader br = new BinaryReader(fs);
                Byte[] textData = br.ReadBytes((int)fs.Length);


                int[] rowList = gridView1.GetSelectedRows();
                if (rowList == null)
                {
                    return;
                }
                foreach (int row in rowList)
                {
                    // DataRow dr = gridView1.GetDataRow(row);
                    DataRow dr = _archives.Tables["infoManArchives"].Rows[row];

                    dr["gImage"] = textData;
                    br.Close();
                    fs.Close();

                    dr.EndEdit();
                    pictureBox1.ImageLocation = fileName;
                    pictureBox1.Refresh();
                }
            }
        }

        private void fm_cl_Load(object sender, EventArgs e)
        {
            this.init();
            this.initimage();
            lookUpEdit7.Focus();
        }
    }
}
