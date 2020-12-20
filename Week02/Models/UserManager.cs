using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Week02.Models
{
    public class UserManager
    {
        Model1 model;
        public UserManager()
        {
            model = new Model1();
        }

        //public String CheckNullRegister(RegisterModel registerModel)
        //{
        //    if(registerModel.Email == null || registerModel.Email.Equals(""))
        //    {
        //        return "Email is empty";
        //    }

        //    if(registerModel.MK == null || registerModel.MK.Equals(""))
        //    {
        //        return "Password is empty";
        //    }

        //    if(registerModel.CFMK == null || registerModel.CFMK.Equals(""))
        //    {
        //        return "Confirm Password is empty";
        //    }

        //    if(registerModel.Ten == null || registerModel.Ten.Equals(""))
        //    {
        //        return "Name is empty";
        //    }

        //    if(registerModel.Diachi == null || registerModel.Diachi.Equals(""))
        //    {
        //        return "Address is empty";
        //    }

        //    if(registerModel.SDT == null || registerModel.SDT.Equals(""))
        //    {
        //        return "Phone is empty";
        //    }

        //    if(registerModel.Ngaysinh == null || registerModel.Ngaysinh.ToString("dd/MM/yyyy hh:mm:ss tt").CompareTo("01/01/0001 12:00:00 AM") == 0 )
        //    {
        //        return "Birthday is empty";
        //    }
        //    return "";
        //}

        //public String CheckMinMaxRegister(RegisterModel registerModel)
        //{
        //    if (registerModel.Email.Length > 50)
        //    {
        //        return "Email must be under 50 characters";
        //    }

        //    if (registerModel.MK.Length > 65)
        //    {
        //        return "Password must be under 65 characters";
        //    }

        //    if (registerModel.CFMK.Length > 65)
        //    {
        //        return "Confirm Password must be under 65 characters";
        //    }

        //    if (registerModel.Ten.Length > 50)
        //    {
        //        return "Name must be under 50 characters";
        //    }

        //    if (registerModel.Diachi.Length > 50)
        //    {
        //        return "Address must be under 50 characters";
        //    }

        //    if (registerModel.SDT.Length > 10)
        //    {
        //        return "Phone must be under 10 characters";
        //    }

        //    return "";
        //}

        //public String CheckNullLogin(LoginModel loginModel)
        //{
        //    if (loginModel.Email == null || loginModel.Email.Equals(""))
        //    {
        //        return "Email is empty";
        //    }

        //    if (loginModel.MK == null || loginModel.MK.Equals(""))
        //    {
        //        return "Password is empty";
        //    }
        //    return "";
        //}

        //public String CheckMinMaxLogin(LoginModel loginModel)
        //{
        //    if (loginModel.Email.Length > 50)
        //    {
        //        return "Email must be under 50 characters";
        //    }

        //    if (loginModel.MK.Length > 65)
        //    {
        //        return "Password must be under 65 characters";
        //    }
        //    return "";
        //}

        public bool CheckEmail(String email)
        {
            var kh = model.KHACHHANGs.Where(a => a.Email == email).ToList();
            if (kh.Count > 0)
                return false;
            return true;
        }

        public bool CheckPhone(String phone)
        {
            var kh = model.KHACHHANGs.Where(a => a.SDT == phone).ToList();
            if (kh.Count > 0)
                return false;
            return true;
        }

        public KHACHHANG CheckLogin(LoginModel loginModel)
        {
            var kh = model.KHACHHANGs.Where(a => a.Email == loginModel.Email).FirstOrDefault();
            if(kh!=null)
            {
                if (kh.MK == loginModel.MK)
                    return kh;
                else return null;
            }
            return null;
        }

        public String GetKH_ID()
        {
            List<KHACHHANG> kHACHHANGs = model.KHACHHANGs.ToList();
            int mkh = 0;
            if (kHACHHANGs.Count() > 0)
            {
                mkh = Int32.Parse(kHACHHANGs[kHACHHANGs.Count - 1].KH_ID.Replace("K", "")) + 1;
            }
            else
            {
                mkh = 1;
            }
            String makh = "K0000";
            return makh.Substring(0, makh.Length - mkh.ToString().Length) + mkh;
        }

        public String GetMAHD_ID()
        {
            List<HOADON> hOADONs = model.HOADONs.ToList();
            int mhd = 0;
            if (hOADONs.Count() > 0)
            {
                mhd = Int32.Parse(hOADONs[hOADONs.Count - 1].MAHD.Replace("H", "")) + 1;
            }
            else
            {
                mhd = 1;
            }
            String mahd = "H0000";
            mahd = mahd.Remove(mahd.Length - mhd.ToString().Length) + mhd;
            return mahd;
        }
    }
}