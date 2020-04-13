using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using DBSystem.BLL;
using DBSystem.ENTITIES;
using System.Data.Entity.Validation;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Core;

namespace MatFSISExercisesCustom.Pages
{ 
    public partial class CRUDPage : System.Web.UI.Page
{
    static string pagenum = "";
    static string pid = "";
    static string add = "";
    List<string> errormsgs = new List<string>();
    private static List<PlayerEntity> PlayerEntityList = new List<PlayerEntity>();
    protected void Page_Load(object sender, EventArgs e)
    {
        Message.DataSource = null;
        Message.DataBind();
        if (!Page.IsPostBack)
        {
            pagenum = Request.QueryString["page"];
            pid = Request.QueryString["pid"];
            add = Request.QueryString["add"];
            BindGuardianList();
            BindTeamList();
            if (string.IsNullOrEmpty(pid))
            {
                Response.Redirect("~/Default.aspx");
            }
            else if (add == "yes")
            {
                UpdateButton.Enabled = false;
                DeleteButton.Enabled = false;
            }
            else
            {
                AddButton.Enabled = false;
                PlayerController sysmgr = new PlayerController();
                PlayerEntity info = null;
                info = sysmgr.FindByPKID(int.Parse(pid));
                if (info == null)
                {
                    errormsgs.Add("Record is no longer on file.");
                    LoadMessageDisplay(errormsgs, "alert alert-info");
                    Clear_Click(sender, e);
                }
                else
                {
                        string genderString = info.Gender;
                    PlayerID.Text = info.PlayerID.ToString();
                    FirstName.Text = info.FirstName;
                    LastName.Text = info.LastName;
                    Age.Text = info.Age.ToString();
                    Gender.Text = info.Gender;
                    AlbertaHealthCareNumber.Text = info.AlbertaHealthCareNumber;
                    MedicalAlertDetails.Text = info.MedicalAlertDetails == null ? "" : info.MedicalAlertDetails;

                    if (info.GuardianID.HasValue)
                    {
                        GuardianList.SelectedValue = info.GuardianID.ToString();
                    }
                    else
                    {
                        GuardianList.SelectedIndex = 0;
                    }
                    if (info.TeamID.HasValue)
                    {
                        TeamList.SelectedValue = info.TeamID.ToString();
                    }
                    else
                    {
                        TeamList.SelectedIndex = 0;
                    }
                }
            }
        }
    }
    protected Exception GetInnerException(Exception ex)
    {
        while (ex.InnerException != null)
        {
            ex = ex.InnerException;
        }
        return ex;
    }
    protected void LoadMessageDisplay(List<string> errormsglist, string cssclass)
    {
        Message.CssClass = cssclass;
        Message.DataSource = errormsglist;
        Message.DataBind();
    }
    protected void BindGuardianList()
    {
        try
        {
            GuardianController sysmgr = new GuardianController();
            List<GuardianEntity> info = null;
            info = sysmgr.List();
            info.Sort((x, y) => x.LastName.CompareTo(y.LastName));
            GuardianList.DataSource = info;
            GuardianList.DataTextField = nameof(GuardianEntity.GuardianFullName);
            GuardianList.DataValueField = nameof(GuardianEntity.GuardianID);
            GuardianList.DataBind();
            GuardianList.Items.Insert(0, "select...");
        }
        catch (Exception ex)
        {
            errormsgs.Add(GetInnerException(ex).ToString());
            LoadMessageDisplay(errormsgs, "alert alert-danger");
        }
    }
    protected void BindTeamList()
    {
        try
        {
            TeamController sysmgr = new TeamController();
            List<TeamEntity> info = null;
            info = sysmgr.List();
            info.Sort((x, y) => x.TeamName.CompareTo(y.TeamName));
            TeamList.DataSource = info;
            TeamList.DataTextField = nameof(TeamEntity.TeamName);
            TeamList.DataValueField = nameof(TeamEntity.TeamID);
            TeamList.DataBind();
            TeamList.Items.Insert(0, "select...");

        }
        catch (Exception ex)
        {
            errormsgs.Add(GetInnerException(ex).ToString());
            LoadMessageDisplay(errormsgs, "alert alert-danger");
        }
    }
    protected void Validation(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(FirstName.Text))
        {
            errormsgs.Add("First name is required");
        }

        if (string.IsNullOrEmpty(LastName.Text))
        {
            errormsgs.Add("Last name is required");
        }


            if (GuardianList.SelectedIndex == 0)
        {
            errormsgs.Add("Category is required");
        }

        if (string.IsNullOrEmpty(Age.Text))
            {
                errormsgs.Add("Age is required");
            }

        if ((Gender.Text != "m" && Gender.Text != "M" && Gender.Text != "F" && Gender.Text != "f" && Gender.Text != "t" && Gender.Text != "T") || Gender.Text.Length != 1)
            {
                errormsgs.Add("Gender must be m, f or t");
            }

        //if (Gender.Text.Length != 1)
        //    {
        //        errormsgs.Add("Please enter a single letter for gender (m, f or t)");
        //    }

    }
    protected void Back_Click(object sender, EventArgs e)
    {
        if (pagenum == "4")
        {
            Response.Redirect("Exercise9and10.aspx");
        }
        else
        {
            Response.Redirect("~/Default.aspx");
        }
    }
    protected void Clear_Click(object sender, EventArgs e)
    {
        PlayerID.Text = "";
        FirstName.Text = "";
        LastName.Text = "";
        Age.Text = "";
        Gender.Text = "";
        AlbertaHealthCareNumber.Text = "";
        MedicalAlertDetails.Text = "";
        GuardianList.ClearSelection();
            TeamList.ClearSelection();
    }
    protected void Add_Click(object sender, EventArgs e)
    {
        Validation(sender, e);
        if (errormsgs.Count > 0)
        {
            LoadMessageDisplay(errormsgs, "alert alert-info");
        }
        else
        {
            try
            {
                PlayerController sysmgr = new PlayerController();
                PlayerEntity player = new PlayerEntity();
                player.FirstName = FirstName.Text.Trim();
                player.LastName = LastName.Text.Trim();
                player.TeamID = int.Parse(TeamList.SelectedValue);
                player.GuardianID = int.Parse(GuardianList.SelectedValue);
                player.Age = int.Parse(Age.Text.Trim());
                //player.Gender = char.Parse(Gender.Text.Trim());
                    player.Gender = Gender.Text.Trim();
                player.AlbertaHealthCareNumber = AlbertaHealthCareNumber.Text.Trim();

                player.MedicalAlertDetails =
                        string.IsNullOrEmpty(MedicalAlertDetails.Text) ? null : MedicalAlertDetails.Text.Trim();

                int newID = sysmgr.Add(player);
                PlayerID.Text = newID.ToString();
                errormsgs.Add("Record has been added");
                LoadMessageDisplay(errormsgs, "alert alert-success");
                UpdateButton.Enabled = true;
                DeleteButton.Enabled = true;
            }
            catch (Exception ex)
            {
                errormsgs.Add(GetInnerException(ex).ToString());
                LoadMessageDisplay(errormsgs, "alert alert-danger");
            }
        }
    }
    protected void Update_Click(object sender, EventArgs e)
    {
        int id = 0;
        if (string.IsNullOrEmpty(PlayerID.Text))
        {
            errormsgs.Add("Search for a record to update");
        }
        else if (!int.TryParse(PlayerID.Text, out id))
        {
            errormsgs.Add("Id is invalid");
        }
        Validation(sender, e);
        if (errormsgs.Count > 0)
        {
            LoadMessageDisplay(errormsgs, "alert alert-info");
        }
        else
        {
            try
            {
                PlayerController sysmgr = new PlayerController();
                PlayerEntity player = new PlayerEntity();
                player.PlayerID = int.Parse(PlayerID.Text);
                    player.FirstName = FirstName.Text.Trim();
                    player.LastName = LastName.Text.Trim();
                    player.TeamID = int.Parse(TeamList.SelectedValue);
                    player.GuardianID = int.Parse(GuardianList.SelectedValue);
                    player.Age = int.Parse(Age.Text.Trim());
                    //player.Gender = char.Parse(Gender.Text.Trim());
                    player.Gender = Gender.Text.Trim();

                    player.AlbertaHealthCareNumber = AlbertaHealthCareNumber.Text.Trim();

                    player.MedicalAlertDetails =
                            string.IsNullOrEmpty(MedicalAlertDetails.Text) ? null : MedicalAlertDetails.Text.Trim();
                    int rowsaffected = sysmgr.Update(player);

                if (rowsaffected > 0)
                {
                    errormsgs.Add("Record has been updated");
                    LoadMessageDisplay(errormsgs, "alert alert-success");
                }
                else
                {
                    errormsgs.Add("Record was not found");
                    LoadMessageDisplay(errormsgs, "alert alert-warning");
                }
            }
            catch (Exception ex)
            {
                errormsgs.Add(GetInnerException(ex).ToString());
                LoadMessageDisplay(errormsgs, "alert alert-danger");
            }
        }
    }
    protected void Delete_Click(object sender, EventArgs e)
    {
        int id = 0;
        if (string.IsNullOrEmpty(PlayerID.Text))
        {
            errormsgs.Add("Search for a record to delete");
        }
        else if (!int.TryParse(PlayerID.Text, out id))
        {
            errormsgs.Add("Id is invalid");
        }
        if (errormsgs.Count > 0)
        {
            LoadMessageDisplay(errormsgs, "alert alert-info");
        }
        else
        {
            try
            {
                PlayerController sysmgr = new PlayerController();
                int rowsaffected = sysmgr.Delete(id);
                if (rowsaffected > 0)
                {
                    errormsgs.Add("Record has been deleted");
                    LoadMessageDisplay(errormsgs, "alert alert-success");
                    Clear_Click(sender, e);
                }
                else
                {
                    errormsgs.Add("Record was not found");
                    LoadMessageDisplay(errormsgs, "alert alert-warning");
                }
                UpdateButton.Enabled = false;
                DeleteButton.Enabled = false;
                AddButton.Enabled = true;

            }
            catch (Exception ex)
            {
                errormsgs.Add(GetInnerException(ex).ToString());
                LoadMessageDisplay(errormsgs, "alert alert-danger");
            }
        }
    }
}
}