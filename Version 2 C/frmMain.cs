using System;
using System.Windows.Forms;

namespace Version_2_C
{
    sealed partial class frmMain : Form
    {        
        private frmMain()
        {
            InitializeComponent();
        }

        private clsArtistList _ArtistList = new clsArtistList();
        public delegate void Notify(string prGalleryName);
        public event Notify GalleryNameChanged;
        private static readonly frmMain _Instance = new frmMain();

        internal static frmMain Instance => _Instance;
        private void updateTitle(string prGalleryName)
        {
            if (!string.IsNullOrEmpty(prGalleryName))
                Text = "Gallery - " + prGalleryName;
        }

        public void updateDisplay()
        {
            lstArtists.DataSource = null;
            string[] lcDisplayList = new string[_ArtistList.Count];
            _ArtistList.Keys.CopyTo(lcDisplayList, 0);
            lstArtists.DataSource = lcDisplayList;
            lblValue.Text = Convert.ToString(_ArtistList.GetTotalValue());
        }
      


        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                frmArtist.Run(new clsArtist(_ArtistList));
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
                    frmArtist.Run(_ArtistList[lcKey]);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "This should never occur");
                }
        }

        private void btnQuit_Click(object sender, EventArgs e)
        {
            try
            {
                _ArtistList.Save();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "File Save Error");
            }
            Close();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string lcKey;

            lcKey = Convert.ToString(lstArtists.SelectedItem);
            if (lcKey != null && MessageBox.Show("Are you sure?", "Deleting artist", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                try
                {
                    _ArtistList.Remove(lcKey);
                    lstArtists.ClearSelected();
                    updateDisplay();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error deleing artist");
                }
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
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
            updateDisplay();
           
        }

        private void btnChangeName_Click(object sender, EventArgs e)
        {
                InputBox inputBox = new InputBox("Insert new Gallery Name");
                _ArtistList.GalleryName = inputBox.Answer;
                GalleryNameChanged(_ArtistList.GalleryName);
        }
    }
}