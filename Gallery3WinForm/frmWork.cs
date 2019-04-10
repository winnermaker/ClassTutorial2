using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Gallery3WinForm
{
    public partial class frmWork : Form
    {
        protected clsAllWork _Work;        

        public frmWork()
        {
            InitializeComponent();
        }

        public void SetDetails(clsAllWork prWork)
        {
            _Work = prWork;
            updateForm();
            ShowDialog();
        }

        private async void btnOK_ClickAsync(object sender, EventArgs e)
        {
            if (isValid())
            {
                pushData();
                if (txtName.Enabled)
                    MessageBox.Show(await ServiceClient.InsertWorkAsync(_Work));
                else
                    MessageBox.Show(await ServiceClient.UpdateWorkAsync(_Work));
                Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        public virtual bool isValid()
        {
            return true;
        }

        protected virtual void updateForm()
        {
            txtName.Text = _Work.Name;
            //txtCreation.Text = _Work.Date.ToShortDateString();
            dateCreation.Value = _Work.Date;
            txtValue.Text = _Work.Value.ToString();
            txtName.Enabled = string.IsNullOrEmpty(_Work.Name);
        }

        protected virtual void pushData()
        {
            _Work.Name = txtName.Text;
            //_Work.Date = DateTime.Parse(txtCreation.Text);
            _Work.Date = dateCreation.Value;
            _Work.Value = decimal.Parse(txtValue.Text);
        }
        public delegate void LoadWorkFormDelegate(clsAllWork prWork);
        public static Dictionary<char, Delegate> _WorksForm = new Dictionary<char, Delegate>
        {
            {'P', new LoadWorkFormDelegate(frmPainting.Run)},
            {'H', new LoadWorkFormDelegate(frmPhotograph.Run)},
            {'S', new LoadWorkFormDelegate(frmSculpture.Run)}
        };
        public static void DispatchWorkForm(clsAllWork prWork)
        {
            _WorksForm[prWork.WorkType].DynamicInvoke(prWork);
        }
    }
}