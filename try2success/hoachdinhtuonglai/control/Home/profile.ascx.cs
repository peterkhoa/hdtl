using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using hoachdinhtuonglai.Data.BL;
using hoachdinhtuonglai.Data.Core;

namespace hoachdinhtuonglai.control.Home
{
    public partial class profile : BaseUserControl
    {
        protected Account account = new Account();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Current_user != null && Current_user.ID > 0)
            {
                account = Current_user;
                if (!IsPostBack)
                {
                    txtAddress.Text = Current_user.Address;
                    txtBD.Text = Current_user.Birthday.HasValue == true ? Current_user.Birthday.Value.ToString("dd/MM/yyyy") : "";
                    txtCompany.Text = Current_user.Company;
                    txtDream.Text = Current_user.Dream;
                    txtfacebook.Text = Current_user.Facebook;
                    txtFirstName.Text = Current_user.FirstName;
                    txtSlogan.Text = Current_user.Slogan;
                    txtSothich.Text = Current_user.SoThich;
                    txtyahoo.Text = Current_user.Yahoo;
                    txtLastName.Text = Current_user.LastName;
                    txtEmail.Text = Current_user.Email;
                    txtPhone.Text = Current_user.PhoneNumber;
                    DropCity.SelectedValue = Current_user.ProvinceSlug;
                    DropGender.SelectedValue = Current_user.Gender;
                    txtSchool.Text = Current_user.School;
                    txtSkype.Text = Current_user.Skype;
                    txtUsername.Text = Current_user.Username;
                }
            }
        }

        protected bool validate()
        {
            string avatar = "";
            if (!string.IsNullOrEmpty(txtFirstName.Text.Trim()))
            {
                account.FirstName = txtFirstName.Text.Trim();
            }
            else
            {
                Utility.CreateAlert(LabelMessage, "Bạn chưa điền họ");
                return false;
            }

            if (!string.IsNullOrEmpty(txtLastName.Text.Trim()))
            {
                account.LastName = txtLastName.Text.Trim();
            }
            else
            {
                Utility.CreateAlert(LabelMessage, "Bạn chưa điền tên");
                return false;
            }

            if (!string.IsNullOrEmpty(txtAvatar.Text.Trim()))
            {
                avatar = txtAvatar.Text.Trim();
                if (avatar.LastIndexOf(".jpg") > 0 || avatar.LastIndexOf(".png") > 0 || avatar.LastIndexOf(".gif") > 0)
                {

                }
                else
                {
                    avatar = "";
                }
            }

            account.Location = "vietnam";
            account.PhoneNumber = txtPhone.Text;
            account.Gender = DropGender.SelectedValue;
            account.Facebook = txtfacebook.Text.Trim();
            account.IP = Request.UserHostAddress;
            account.Province = DropCity.SelectedItem.Text;
            account.ProvinceSlug = DropCity.SelectedValue;
            account.School = txtSchool.Text.Trim();
            account.SchoolSlug = Library.LanguageConvert.RemoveDiacritics(txtSchool.Text.Trim());
            account.Skype = txtSkype.Text.Trim();
            account.Slogan = txtSlogan.Text.Trim();
            account.SoThich = txtSothich.Text.Trim();
            account.Yahoo = txtyahoo.Text.Trim();
            account.LastLogin = DateTime.Now;
            account.Dream = txtDream.Text.Trim();
            account.Company = txtCompany.Text.Trim();
            account.Address = txtAddress.Text.Trim();
            account.Avatar = avatar;
            try
            {
                DateTime birthday = DateTime.ParseExact(txtBD.Text, "MM/dd/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                account.Birthday = birthday;
            }
            catch { }
            
            return true;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (validate())
            {
                AccountDA.Update(account);
                Utility.CreateInfo(LabelMessage,"Cập nhật thông tin thành công");
            }
        }
    }
}