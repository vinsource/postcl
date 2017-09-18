using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using VinCLAPP.DatabaseModel;
using VinCLAPP.Helper;
using VinCLAPP.Model;
using City = VinCLAPP.DatabaseModel.City;

namespace VinCLAPP
{
    public partial class CityChoiceForm : Form
    {
        private readonly MainForm _frmMain;
        public CityChoiceForm()
        {
            InitializeComponent();
        }

        public CityChoiceForm(MainForm frmMain)
        {
            InitializeComponent();
            this._frmMain = frmMain;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            var cities = GetSelectedCity();
            if (cities.Any())
            {
                DeleteCurrentCities();
                SaveSelectedCity(cities);

                var selectedCity = (PostClCity) cbCityList.SelectedItem;

                var context = new CLDMSEntities();

                var account =
                    context.Accounts.FirstOrDefault(o => o.AccountId == GlobalVar.CurrentAccount.AccountId);

                if (account != null)
                {
                    account.FirstCity = selectedCity.CityId;
                    account.LastUpdated = DateTime.Now;
                    context.SaveChanges();

                    ReOrderCityList(selectedCity.CityId);

                    _frmMain.AfterLogging();
                }
                Close();
            }
            else
            {

                MessageBox.Show("Please choose at least one city in the list", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
         
          
        }

        private void DeleteCurrentCities()
        {
            var context = new CLDMSEntities();
            var item =
                context.SelectedCities.Where(
                    i =>
                    i.AccountId == GlobalVar.CurrentAccount.AccountId &&
                    i.DealerId == GlobalVar.CurrentAccount.DealerId);
            foreach (var city in item)
            {
                context.SelectedCities.Remove(city);
            }
            if (item.Any())
            {
                context.SaveChanges();
            }
        }

        private static void SaveSelectedCity(IEnumerable<int> cities)
        {
            var context = new CLDMSEntities();
            foreach (int city in cities)
            {
                context.SelectedCities.Add(new SelectedCity()
                    {
                        AccountId = GlobalVar.CurrentAccount.AccountId,
                        CityId = city,
                        DateStamp = DateTime.Now,
                        DealerId = GlobalVar.CurrentAccount.DealerId
                    });
            }
            context.SaveChanges();
        }

        private void CityChoiceForm_Load(object sender, EventArgs e)
        {
            IniteStatelist();
            cbCityList.Items.Clear();
            foreach (TreeNode stateNode in StateTreeview.Nodes)
            {
                foreach (TreeNode cityNode in stateNode.Nodes)
                {
                    if (cityNode.Checked)
                    {
                        var city = new PostClCity()
                        {
                            CityName = cityNode.Text,
                            StateName = stateNode.Text,
                            CityId = (int)cityNode.Tag
                        };

                        cbCityList.Items.Add(city);
                    }
                }

            }



            if (cbCityList.Items.Count > 0)
                cbCityList.SelectedIndex = 0;
        }

        private void IniteStatelist()
        {
            var context = new CLDMSEntities();
         
            var dtDealerCityAccount =
                context.SelectedCities.Where(x => x.AccountId == GlobalVar.CurrentAccount.AccountId).ToList();

            IQueryable<IGrouping<string, City>> statelist =
                context.Cities.OrderBy(j => j.State).GroupBy(i => i.State);
            
            foreach (var state in statelist)
            {
                var treeNode = new TreeNode(state.Key,
                                            state.OrderBy(i => i.CityName)
                                                 .Select(
                                                     city =>
                                                     new TreeNode(city.CityName)
                                                         {
                                                             Tag = city.CityID,
                                                             Checked =
                                                                 dtDealerCityAccount.FirstOrDefault(
                                                                     i => i.CityId == city.CityID) != null
                                                         })
                                                 .ToArray());
                StateTreeview.Nodes.Add(treeNode);
                GridviewHelper.HideCheckBox(StateTreeview, treeNode);
            }
        }


        private void ReOrderCityList(int cityId)
        {
            var firstCity = GlobalVar.CurrentDealer.CityList.FirstOrDefault(x => x.CityID == cityId);
            IEnumerable<Model.City> restCity = GlobalVar.CurrentDealer.CityList.Where(x => x.CityID != cityId);
            GlobalVar.CurrentDealer.CityList = new List<Model.City> {firstCity};
            GlobalVar.CurrentDealer.CityList = GlobalVar.CurrentDealer.CityList.Concat(restCity).ToList();

        }

        private List<int> GetSelectedCity()
        {
            var states = new List<int>();
            TreeNodeCollection nodes = StateTreeview.Nodes;
            foreach (TreeNode node in nodes)
            {
                GetNodeRecursive(node, states);
            }
            return states;
        }

        private void GetNodeRecursive(TreeNode treeNode, List<int> states)
        {
            if (treeNode.Checked)
            {
                states.Add((int) treeNode.Tag);
              
            }
            foreach (TreeNode tn in treeNode.Nodes)
            {
                GetNodeRecursive(tn, states);
            }
        }

        private void StateTreeview_AfterCheck(object sender, TreeViewEventArgs e)
        {
            cbCityList.Items.Clear();
            foreach (TreeNode stateNode in StateTreeview.Nodes)
            {
                foreach (TreeNode cityNode in stateNode.Nodes)
                {
                    if (cityNode.Checked)
                    {
                        var city = new PostClCity()
                            {
                                CityName = cityNode.Text,
                                StateName = stateNode.Text,
                                CityId = (int) cityNode.Tag
                            };

                        cbCityList.Items.Add(city);
                    }
                }
             
            }

            

            if (cbCityList.Items.Count > 0)
                cbCityList.SelectedIndex = 0;


        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            ResetAllTreeNodes();
            cbCityList.Items.Clear();
           
        }

        private void ResetAllTreeNodes()
        {
            TreeNodeCollection nodes = StateTreeview.Nodes;
            foreach (TreeNode node in nodes)
            {
              UnCheckAllTreeNodes(node);
            }
        }

        private void UnCheckAllTreeNodes(TreeNode treeNode)
        {
            treeNode.Checked = false;
            foreach (TreeNode tn in treeNode.Nodes)
            {
                UnCheckAllTreeNodes(tn);
            }
        }
    }

    
}