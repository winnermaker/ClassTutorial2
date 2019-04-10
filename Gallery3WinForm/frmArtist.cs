using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Gallery3WinForm
{
    public partial class frmArtist : Form
    {
        public frmArtist()
        {
            InitializeComponent();
        }

        private clsArtist _Artist;
        //private clsWorksList _WorksList;
        private static Dictionary<string, frmArtist> _ArtistFormList = new Dictionary<string, frmArtist>();

        private void updateTitle(string prGalleryName)
        {
            if (!string.IsNullOrEmpty(prGalleryName))
                Text = "Artist Details - " + prGalleryName;
        }

        public static void Run(string prArtistName)
        {
            frmArtist lcArtistForm;
            if (string.IsNullOrEmpty(prArtistName) ||
            !_ArtistFormList.TryGetValue(prArtistName, out lcArtistForm))
            {
                lcArtistForm = new frmArtist();
                if (string.IsNullOrEmpty(prArtistName))
                    lcArtistForm.SetDetails(new clsArtist());
                else
                {
                    _ArtistFormList.Add(prArtistName, lcArtistForm);
                    lcArtistForm.refreshFormFromDBAsync(prArtistName);
                }
            }
            else
            {
                lcArtistForm.Show();
                lcArtistForm.Activate();
            }
        }

        private async void refreshFormFromDBAsync(string prArtistName)
        {
            SetDetails(await ServiceClient.GetArtistAsync(prArtistName));
        }

        private void updateDisplay()
        {
            lstWorks.DataSource = null;
            if (_Artist.WorksList != null)
                lstWorks.DataSource = _Artist.WorksList;       
           
            
            /*if (_WorksList.SortOrder == 0)
            {
                _WorksList.SortByName();
                rbByName.Checked = true;
            }
            else
            {
                _WorksList.SortByDate();
                rbByDate.Checked = true;
            }

            lstWorks.DataSource = null;
            lstWorks.DataSource = _WorksList;
            lblTotal.Text = Convert.ToString(_WorksList.GetTotalValue());
            */
        }

        public void SetDetails(clsArtist prArtist)
        {
            _Artist = prArtist;
            txtName.Enabled = string.IsNullOrEmpty(_Artist.Name);
            frmMain.Instance.GalleryNameChanged += new frmMain.Notify(updateTitle);
            //updateTitle(_Artist.ArtistList.GalleryName);
            updateForm();
            updateDisplay();
            Show();
        }

        private void updateForm()
        {
            txtName.Text = _Artist.Name;
            txtSpeciality.Text = _Artist.Speciality;
            txtPhone.Text = _Artist.Phone;
           
            //_WorksList = _Artist.WorksList;
        }

        private void pushData()
        {
            _Artist.Name = txtName.Text;
            _Artist.Speciality = txtSpeciality.Text;
            _Artist.Phone = txtPhone.Text;
        }

        private async void btnDelete_ClickAsync(object sender, EventArgs e)
        {
            int lcIndex = lstWorks.SelectedIndex;

            if (lcIndex >= 0 && MessageBox.Show("Are you sure?", "Deleting work", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                MessageBox.Show(await ServiceClient.DeleteArtworkAsync(lstWorks.SelectedItem as clsAllWork));
                refreshFormFromDBAsync(_Artist.Name);
                frmMain.Instance.UpdateDisplayAsync();
            }
        }

        private async void btnAdd_ClickAsync(object sender, EventArgs e)
        {
            try
            {
                string lcReply = new InputBox(clsAllWork.FACTORY_PROMPT).Answer;
                if (!string.IsNullOrEmpty(lcReply)) // not cancelled?
                {
                    clsAllWork lcWork = clsAllWork.NewWork(lcReply[0]);
                    lcWork.Date = DateTime.Now;
                    if (lcWork != null) // valid artwork created?
                    {
                        if (txtName.Enabled) // new artist not saved?
                        {
                            pushData();
                            await ServiceClient.InsertArtistAsync(_Artist);
                            txtName.Enabled = false;
                        }
                        lcWork.ArtistName = _Artist.Name;
                        frmWork.DispatchWorkForm(lcWork);
                        if (!string.IsNullOrEmpty(lcWork.Name)) // not cancelled?
                        {
                            refreshFormFromDBAsync(_Artist.Name);
                            frmMain.Instance.UpdateDisplayAsync();
                        }
                    }
                }            
            }
            catch(Exception)
            {

            }
            /*
             string lcReply = new InputBox(clsWork.FACTORY_PROMPT).Answer;
             if (!string.IsNullOrEmpty(lcReply))
             {
                 _WorksList.AddWork(lcReply[0]);
                 updateDisplay();
                 frmMain.Instance.updateDisplay();
             }
             */
        }

        private async void btnClose_ClickAsync(object sender, EventArgs e)
        {
            pushData();
            if (txtName.Enabled)
            {
                MessageBox.Show(await ServiceClient.InsertArtistAsync(_Artist));
                frmMain.Instance.UpdateDisplayAsync();
                txtName.Enabled = false;
            }
            else
                MessageBox.Show(await ServiceClient.UpdateArtistAsync(_Artist));
            Hide();
            /*
            if (isValid() == true)
                try
                {
                    pushData();
                    if (txtName.Enabled)
                    {
                        Artist.NewArtist();
                        MessageBox.Show("Artist added!", "Success");
                        frmMain.Instance.updateDisplay();
                        txtName.Enabled = false;
                    }
                    Hide();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                */
        }

        private Boolean isValid()
        {
            /*
            if (txtName.Enabled && txtName.Text != "")
                if (_Artist.IsDuplicate(txtName.Text))
                {
                    MessageBox.Show("Artist with that name already exists!", "Error adding artist");
                    return false;
                }
                else
                    return true;
            else */
                return true;
                
        }

        private void lstWorks_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                frmWork.DispatchWorkForm(lstWorks.SelectedValue as clsAllWork);
                //_WorksList.EditWork(lstWorks.SelectedIndex);
                updateDisplay();
                //frmMain.Instance.updateDisplay();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void rbByDate_CheckedChanged(object sender, EventArgs e)
        {
           // _WorksList.SortOrder = Convert.ToByte(rbByDate.Checked);
            updateDisplay();
        }
    }
}