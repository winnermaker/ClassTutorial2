using System;
using System.Windows.Forms;

namespace Gallery3WinForm
{
    sealed partial class frmMain : Form
    {        
        private frmMain()
        {
            InitializeComponent();
        }

        //private clsArtistList _ArtistList = new clsArtistList();
        public delegate void Notify(string prGalleryName);
        public event Notify GalleryNameChanged;
        private static readonly frmMain _Instance = new frmMain();

        internal static frmMain Instance => _Instance;
        private void updateTitle(string prGalleryName)
        {
            if (!string.IsNullOrEmpty(prGalleryName))
                Text = "Gallery - " + prGalleryName;
        }

        public async void UpdateDisplayAsync()
        {
            try
            {
                lstArtists.DataSource = null;
                lstArtists.DataSource = await ServiceClient.GetArtistNamesAsync();
            }
            catch
            {

            }            
            /*
            lstArtists.DataSource = null;
            string[] lcDisplayList = new string[_ArtistList.Count];
            _ArtistList.Keys.CopyTo(lcDisplayList, 0);
            lstArtists.DataSource = lcDisplayList;
            lblValue.Text = Convert.ToString(_ArtistList.GetTotalValue());
            */
        }
      


        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                frmArtist.Run(null);               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error adding new artist");
            }
        }

        private void lstArtists_DoubleClick(object sender, EventArgs e)
        {
            string lcKey;

            lcKey = Convert.ToString(lstArtists.SelectedItem);
            if (lcKey != null)
                try
                {
                    frmArtist.Run(lstArtists.SelectedItem as string);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "This should never occur");
                }
                
        }

        private void btnQuit_Click(object sender, EventArgs e)
        {
          Close();
        }

        private async void btnDelete_ClickAsync(object sender, EventArgs e)
        {
            string lcKey;

            lcKey = Convert.ToString(lstArtists.SelectedItem);
            if (lcKey != null && MessageBox.Show("Are you sure?", "Deleting artist", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                try
                {
                    MessageBox.Show(await ServiceClient.DeleteArtistAsync(lcKey));
                    lstArtists.ClearSelected();
                    UpdateDisplayAsync();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error deleing artist");
                }
            
        
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            /*
            try
            {
                _ArtistList = clsArtistList.RetrieveArtistList();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "File retrieve error");
            }
            GalleryNameChanged += new Notify(updateTitle);
            GalleryNameChanged(_ArtistList.GalleryName); // event raising!
            */
            UpdateDisplayAsync();
           
        }

        private void btnChangeName_Click(object sender, EventArgs e)
        {
            /*
                InputBox inputBox = new InputBox("Insert new Gallery Name");
                _ArtistList.GalleryName = inputBox.Answer;
                GalleryNameChanged(_ArtistList.GalleryName);
            */
        }
    }
}