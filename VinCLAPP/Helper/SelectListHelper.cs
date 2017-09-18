using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using VinCLAPP.AutomativeDescriptionService7;
using VinCLAPP.Model;

namespace VinCLAPP.Helper
{
    public class SelectListHelper
    {
        private const string DisplayDefaultSoldOut = "Delete (Default)";
        private const string Display3DaysSoldOut = "Display as Sold (3 Days)";
        private const string Display5DaysSoldOut = "Display as Sold (5 Days)";
        private const string Display7DaysSoldOut = "Display as Sold (7 Days)";
        private const string Display30DaysSoldOut = "Display as Sold (30 Days)";

        private const string OtherTrims = "Base/Other Trims";
        private const string OtherColors = "Other Colors";

        public static IEnumerable<SelectListItem> InitialYearList()
        {
            var returnList = new List<SelectListItem>();

            var autoService = new ChromeAutoService();

            int[] modelYears = autoService.GetModelYears();

            returnList.Add(CreateSelectListItem("Select a year", "Select a year", false));

            foreach (int year in modelYears)
            {
                returnList.Add(CreateSelectListItem(year.ToString(), year.ToString(), false));
            }

            return returnList.AsEnumerable();
        }

        public static IEnumerable<SelectListItem> InitialYearList(IQueryable<int?> modelYears)
        {
            var returnList = new List<SelectListItem>();

            returnList.Add(CreateSelectListItem("Year", "Year", false));

            foreach (int year in modelYears)
            {
                returnList.Add(CreateSelectListItem(year.ToString(), year.ToString(), false));
            }

            return returnList.AsEnumerable();
        }

        //public static IEnumerable<SelectListItem> InitialYearListFromKBB()
        //{
        //    var returnList = new List<SelectListItem>();

        //    returnList.Add(CreateSelectListItem("Year...", "0", false));

        //    var list = KellyBlueBookHelper.GetKBBYears();

        //    foreach (var tmp in list)
        //    {
        //        returnList.Add(CreateSelectListItem(tmp.Value, tmp.Id.ToString(), false));
        //    }
        //    return returnList.AsEnumerable();

        //}

        public static IEnumerable<SelectListItem> InitialYearListFromChrome()
        {
            var returnList = new List<SelectListItem> { CreateSelectListItem("Year...", "0", false) };

            var autoService = new ChromeAutoService();

            int[] list = autoService.GetModelYears();

            foreach (int tmp in list)
            {
                returnList.Add(CreateSelectListItem(tmp.ToString(), tmp.ToString(), false));
            }
            return returnList.AsEnumerable();
        }

        public static IEnumerable<SelectListItem> InitialMakeList(IdentifiedString[] divisionList)
        {
            var returnList = new List<SelectListItem>();

            bool flag = true;

            returnList.Add(CreateSelectListItem("Select a make", "0" + "|" + "Select a make", false));

            foreach (IdentifiedString division in divisionList)
            {
                if (flag)
                {
                    returnList.Add(CreateSelectListItem(division.Value, division.id + "|" + division.Value, false));
                    flag = false;
                }
                else
                    returnList.Add(CreateSelectListItem(division.Value, division.id + "|" + division.Value, false));
            }

            return returnList.AsEnumerable();
        }

        public static IEnumerable<SelectListItem> InitalTrimList(Style[] styleList, bool isOther)
        {
            var returnList = new List<SelectListItem>();
            if (styleList != null && styleList.Any())
            {
                Style firstStyle = styleList.First();

                var hash = new Hashtable();
                int baseOtherTrimId = 0;
                Style item = styleList.FirstOrDefault(i => String.IsNullOrEmpty(i.trim));
                if (item != null)
                {
                    baseOtherTrimId = item.id;
                }
                returnList.Add(CreateSelectListItem("Base/Other Trims",
                                                    (baseOtherTrimId != 0
                                                         ? baseOtherTrimId.ToString()
                                                         : styleList.Select(i => i.id).FirstOrDefault().ToString())
                                                   , false));

                foreach (Style style in styleList)
                {
                    if (!String.IsNullOrEmpty(style.trim) && !hash.Contains(style.trim))
                    {
                        returnList.Add(CreateSelectListItem(style.trim, style.id.ToString(), false));

                        hash.Add(style.trim, style.id.ToString());
                    }
                }
                return returnList.AsEnumerable();
            }

            returnList.Add(CreateSelectListItem("Base/Other Trims", "Base/Other Trims", isOther));
            return returnList;
        }

        public static IEnumerable<SelectListItem> InitalTrimList(Style[] styleList)
        {
            return InitalTrimList(styleList, true);
        }

        public static IEnumerable<SelectListItem> InitalTrimList(IdentifiedString[] styleList)
        {
            var returnList = new List<SelectListItem>();
            if (styleList != null && styleList.Any())
            {
                IdentifiedString firstStyle = styleList.First();

                var hash = new HashSet<string>();

                foreach (IdentifiedString style in styleList)
                {
                    if (!hash.Contains(style.Value))
                    {
                        if (String.IsNullOrEmpty(style.Value))
                        {
                            //returnList.Add(CreateSelectListItem("Base", style.id + "|Base", false));
                            firstStyle = style;
                        }
                        else
                        {
                            returnList.Add(CreateSelectListItem(style.Value, style.id + "|" + style.Value, false));
                        }

                        hash.Add(style.Value);
                    }
                }

                //returnList.Add(CreateSelectListItem(OtherTrims, firstStyle.id + "|" + OtherTrims, false));
                returnList.Add(CreateSelectListItem("Base/Other Trims", firstStyle.id + "|" + "Base/Other Trims", false));
            }

            return returnList;
        }

        public static IEnumerable<SelectListItem> InitalTrimList(ISelectedTrimItem vm, string styleName,
                                                                 Style[] styleList, int chromeStyleId, out bool existed)
        {
            vm.SelectedTrimItem = null;
            existed = false;
            var returnList = new List<SelectListItem>();
            if (styleList != null && styleList.Any())
            {
                var hash = new Hashtable();

                foreach (var trim in styleList)
                {
                    AddTrimByName(returnList, hash, trim, trim.trim);
                }

                int baseOtherTrimId = 0;
                Style item = styleList.FirstOrDefault(i => String.IsNullOrEmpty(i.trim));
                if (item != null)
                {
                    baseOtherTrimId = item.id;
                };
                returnList.Add(CreateSelectListItem("Base/Other Trims",
                                                    (baseOtherTrimId != 0
                                                         ? baseOtherTrimId
                                                         : styleList.Select(i => i.id).FirstOrDefault()).ToString(), false));

                if (vm.SelectedTrimItem == null)
                {
                    SelectListItem firstOrDefault = returnList.FirstOrDefault(i => i.Text.Equals("Base/Other Trims"));
                    if (firstOrDefault != null)
                        vm.SelectedTrimItem = firstOrDefault.Value;
                }

                return returnList.AsEnumerable();
            }

            returnList.Add(CreateSelectListItem("Base/Other Trims", "Base/Other Trims", false));
            return returnList;
        }

        public static IEnumerable<SelectListItem> InitalTrimList(ISelectedTrimItem vm, Style[] styleList,
                                                                 string chromeStyleName)
        {


            var returnList = new List<SelectListItem>();
            if (styleList != null && styleList.Any())
            {
                var hash = new Hashtable();

                foreach (var style in styleList)
                {
                    AddTrimByName(returnList, hash, style, style.trim);
                }

                int baseOtherTrimId = 0;
                Style item = styleList.FirstOrDefault(i => String.IsNullOrEmpty(i.trim));
                if (item != null)
                {
                    baseOtherTrimId = item.id;
                };
                returnList.Add(CreateSelectListItem("Base/Other Trims",
                                                    (baseOtherTrimId != 0
                                                         ? baseOtherTrimId.ToString()
                                                         : styleList.Select(i => i.id).FirstOrDefault().ToString())
                                                    , false));


                return returnList.AsEnumerable();
            }

            returnList.Add(CreateSelectListItem("Base/Other Trims", "Base/Other Trims", false));
            return returnList;
        }

        private static bool AddBaseModel(bool existed, Style[] styleList, ref int baseOtherTrimId)
        {
            Style item = styleList.FirstOrDefault(i => String.IsNullOrEmpty(i.trim));
            if (item != null)
            {
                //returnList.Add(CreateSelectListItem("Base", item.styleId.ToString(CultureInfo.InvariantCulture) + "|" + "Base", styleName.Equals("Base")));
                baseOtherTrimId = item.id;
                //hash.Add("Base", item.styleId.ToString(CultureInfo.InvariantCulture));
                //if(styleName.Equals("Base"))
                //{
                //    existed = true;
                //    vm.SelectedTrimItem = item.styleId.ToString(CultureInfo.InvariantCulture) + "|" + "Base";
                //}
            }
            return existed;
        }

        private static void AddTrimByName(List<SelectListItem> returnList, Hashtable hash, Style trim, string name)
        {
            if (!String.IsNullOrEmpty(name) && !hash.Contains(name))
            {
                returnList.Add(CreateSelectListItem(name, trim.id.ToString(), false));
                hash.Add(name, trim.id.ToString());
            }
        }

        public static IEnumerable<SelectListItem> InitalExteriorColorList(Color[] exteriorColorArray)
        {
            var returnList = new List<SelectListItem>();
            var hash = new Hashtable();

            if (exteriorColorArray != null && exteriorColorArray.Any())
            {
                bool flag = true;

                foreach (Color color in exteriorColorArray.OrderBy(x => x.colorName))
                {
                    if (!hash.Contains(color.colorName))
                    {
                        if (flag)
                        {
                            returnList.Add(CreateSelectListItem(color.colorName, color.colorCode, true));
                            flag = false;
                        }
                        else
                            returnList.Add(CreateSelectListItem(color.colorName, color.colorCode, false));

                        hash.Add(color.colorName, color.colorCode);
                    }
                }

                returnList = returnList.OrderBy(i => i.Text).ToList();
                returnList.Add(CreateSelectListItem(OtherColors, OtherColors, false));
            }

            return returnList.AsEnumerable();
        }

        public static IEnumerable<SelectListItem> InitalInteriorColorList(Color[] interiorColorArray)
        {
            var returnList = new List<SelectListItem>();
            var hash = new Hashtable();

            if (interiorColorArray != null && interiorColorArray.Any())
            {
                bool flag = true;

                foreach (Color color in interiorColorArray.OrderBy(x => x.colorName))
                {
                    if (!hash.Contains(color.colorName))
                    {
                        if (flag)
                        {
                            returnList.Add(CreateSelectListItem(color.colorName, color.colorName, true));
                            flag = false;
                        }
                        else
                            returnList.Add(CreateSelectListItem(color.colorName, color.colorName, false));

                        hash.Add(color.colorName, color.colorName);
                    }
                }

                returnList.Add(CreateSelectListItem(OtherColors, OtherColors, false));

                return returnList.AsEnumerable();
            }

            returnList.Add(CreateSelectListItem(OtherColors, OtherColors, false));
            return returnList.AsEnumerable();
        }

        public static IEnumerable<SelectListItem> InitalExteriorColorList(Color[] exteriorColorArray,
                                                                          string selectedColorCode,
                                                                          string selectedColorName)
        {
            var returnList = new List<SelectListItem>();

            bool flag = true;

            if (!String.IsNullOrEmpty(selectedColorCode))
            {
                if (exteriorColorArray != null && exteriorColorArray.Any())
                {
                    if (
                        exteriorColorArray.Any(
                            x => x.colorCode.ToUpperInvariant().Equals(selectedColorCode.ToUpperInvariant())))
                    {
                        IEnumerable<Color> selectedExteriorColor =
                            exteriorColorArray.Where(
                                x => x.colorCode.ToUpperInvariant().Equals(selectedColorCode.ToUpperInvariant()));

                        IEnumerable<Color> extractExteriorColorList =
                            exteriorColorArray.Where(
                                x => !x.colorCode.ToUpperInvariant().Equals(selectedColorCode.ToUpperInvariant()));

                        var exteriorColorList = new List<Color> { selectedExteriorColor.First() };

                        exteriorColorList.AddRange(extractExteriorColorList);

                        foreach (Color ec in exteriorColorList)
                        {
                            if (flag)
                            {
                                returnList.Add(!String.IsNullOrEmpty(ec.colorCode)
                                                   ? CreateSelectListItem(ec.colorName, ec.colorCode, true)
                                                   : CreateSelectListItem(ec.colorName, ec.colorName, true));
                                flag = false;
                            }
                            else
                            {
                                returnList.Add(!String.IsNullOrEmpty(ec.colorCode)
                                                   ? CreateSelectListItem(ec.colorName, ec.colorCode, false)
                                                   : CreateSelectListItem(ec.colorName, ec.colorName, false));
                            }
                        }

                        returnList = returnList.OrderBy(i => i.Text).ToList();
                        returnList.Add(CreateSelectListItem(OtherColors, OtherColors, false));
                    }
                    else if (
                        exteriorColorArray.Any(
                            x => x.colorName.ToUpperInvariant().Equals(selectedColorCode.ToUpperInvariant())))
                    {
                        IEnumerable<Color> selectedExteriorColor =
                            exteriorColorArray.Where(
                                x => x.colorName.ToUpperInvariant().Equals(selectedColorCode.ToUpperInvariant()));

                        IEnumerable<Color> extractExteriorColorList =
                            exteriorColorArray.Where(
                                x => !x.colorName.ToUpperInvariant().Equals(selectedColorCode.ToUpperInvariant()));

                        var exteriorColorList = new List<Color> { selectedExteriorColor.First() };

                        exteriorColorList.AddRange(extractExteriorColorList);

                        foreach (Color ec in exteriorColorList)
                        {
                            if (flag)
                            {
                                returnList.Add(!String.IsNullOrEmpty(ec.colorCode)
                                                   ? CreateSelectListItem(ec.colorName, ec.colorCode, true)
                                                   : CreateSelectListItem(ec.colorName, ec.colorName, true));
                                flag = false;
                            }
                            else
                            {
                                returnList.Add(!String.IsNullOrEmpty(ec.colorCode)
                                                   ? CreateSelectListItem(ec.colorName, ec.colorCode, false)
                                                   : CreateSelectListItem(ec.colorName, ec.colorName, false));
                            }
                        }

                        returnList = returnList.OrderBy(i => i.Text).ToList();
                        returnList.Add(CreateSelectListItem(OtherColors, OtherColors, false));
                    }
                    else
                    {
                        returnList.Add(CreateSelectListItem(OtherColors, OtherColors, true));

                        foreach (Color ec in exteriorColorArray)
                        {
                            returnList.Add(!String.IsNullOrEmpty(ec.colorCode)
                                               ? CreateSelectListItem(ec.colorName, ec.colorCode, false)
                                               : CreateSelectListItem(ec.colorName, ec.colorName, true));
                        }
                    }
                }
                else
                    returnList.Add(CreateSelectListItem(OtherColors, OtherColors, false));
            }
            else
            {
                if (exteriorColorArray != null && exteriorColorArray.Any())
                {
                    returnList.Add(CreateSelectListItem(OtherColors, OtherColors, true));

                    foreach (Color ec in exteriorColorArray)
                    {
                        returnList.Add(!String.IsNullOrEmpty(ec.colorCode)
                                           ? CreateSelectListItem(ec.colorName, ec.colorCode, false)
                                           : CreateSelectListItem(ec.colorName, ec.colorName, false));
                    }
                }
                else
                    returnList.Add(CreateSelectListItem(OtherColors, OtherColors, false));
            }

            return returnList.AsEnumerable();
        }

        public static IEnumerable<SelectListItem> InitalInteriorColorList(Color[] interiorColorArray,
                                                                          string selectedColor)
        {
            var returnList = new List<SelectListItem>();

            bool flag = true;

            if (interiorColorArray != null && interiorColorArray.Any())
            {
                IEnumerable<Color> selectedInteriorColor =
                    interiorColorArray.Where(
                        x => x.colorName.ToUpperInvariant().Equals(selectedColor.ToUpperInvariant()));

                IEnumerable<Color> extractInteriorColorList =
                    interiorColorArray.Where(
                        x => !x.colorName.ToUpperInvariant().Equals(selectedColor.ToUpperInvariant()));

                if (!selectedInteriorColor.Any())
                {
                    returnList.Add(CreateSelectListItem(OtherColors, OtherColors, true));

                    foreach (Color ec in interiorColorArray)
                    {
                        returnList.Add(CreateSelectListItem(ec.colorName, ec.colorName, false));
                    }
                }
                else
                {
                    var exteriorColorList = new List<Color>();

                    exteriorColorList.Add(selectedInteriorColor.First());

                    exteriorColorList.AddRange(extractInteriorColorList);

                    foreach (Color ec in exteriorColorList)
                    {
                        if (flag)
                        {
                            returnList.Add(CreateSelectListItem(ec.colorName, ec.colorName, true));
                            flag = false;
                        }
                        else
                            returnList.Add(CreateSelectListItem(ec.colorName, ec.colorName, false));
                    }

                    returnList = returnList.OrderBy(i => i.Text).ToList();
                    returnList.Add(CreateSelectListItem(OtherColors, OtherColors, false));
                }
            }
            else
                returnList.Add(CreateSelectListItem(OtherColors, OtherColors, false));

            return returnList.AsEnumerable();
        }

        public static IEnumerable<SelectListItem> InitalWarrantyRadioButtonList(int warrantyInfo)
        {
            var returnList = new List<SelectListItem>();


            return returnList.AsEnumerable();
        }

        public static IEnumerable<SelectListItem> InitialFuelList(Engine[] engineList)
        {
            var returnList = new List<SelectListItem>();

            bool flag = true;

            foreach (Engine er in engineList)
            {
                string fuelType = er.fuelType.Value;
                int index = fuelType.LastIndexOf(" ", StringComparison.Ordinal);
                if (!returnList.Any(i => i.Text.ToLower().Equals(fuelType.Substring(0, index).ToLower())))
                {
                    if (flag)
                    {
                        returnList.Add(CreateSelectListItem(fuelType.Substring(0, index), fuelType.Substring(0, index),
                                                            true));
                        flag = false;
                    }
                    else
                        returnList.Add(CreateSelectListItem(fuelType.Substring(0, index), fuelType.Substring(0, index),
                                                            false));
                }
            }

            return returnList.AsEnumerable();
        }

        public static IEnumerable<SelectListItem> InitialCylinderList(Engine[] engineList)
        {
            var returnList = new List<SelectListItem>();

            bool flag = true;

            foreach (Engine er in engineList)
            {
                if (flag)
                {
                    returnList.Add(CreateSelectListItem(er.cylinders.ToString(), er.cylinders.ToString(), true));
                    flag = false;
                }
                else
                    returnList.Add(CreateSelectListItem(er.cylinders.ToString(), er.cylinders.ToString(), false));
            }

            return returnList.AsEnumerable();
        }

        public static IEnumerable<SelectListItem> InitialLitterList(Engine[] engineList)
        {
            var returnList = new List<SelectListItem>();

            bool flag = true;

            foreach (Engine er in engineList)
            {
                if (flag)
                {
                    returnList.Add(CreateSelectListItem(er.displacement.liters.ToString(),
                                                        er.displacement.liters.ToString(), true));
                    flag = false;
                }
                else
                    returnList.Add(CreateSelectListItem(er.displacement.liters.ToString(),
                                                        er.displacement.liters.ToString(), false));
            }

            return returnList.AsEnumerable();
        }

        public static IEnumerable<SelectListItem> InitialBodyTypeList(string bodyType)
        {
            var returnList = new List<SelectListItem> { CreateSelectListItem(bodyType, bodyType, true) };

            ////bool flag = true;


            //string bodyType = bodyTypeList.First(x => x.primary).bodyTypeName;

            //if (bodyTypeList.Any(x => !x.primary))
            //{
            //    var btmp =
            //        bodyTypeList.First(x => !x.primary);
            //    if (bodyType.Equals("2dr Car"))
            //    {
            //        if (btmp.bodyTypeName.Contains("Convertible"))
            //            returnList.Add(SelectListHelper.createSelectListItem(btmp.bodyTypeName, btmp.bodyTypeName, true));
            //        else
            //        {
            //            returnList.Add(SelectListHelper.createSelectListItem("Coupe", "Coupe", true));
            //        }
            //    }
            //}
            //else
            //{
            //    if (bodyType.Equals("2dr Car"))
            //    {

            //        returnList.Add(createSelectListItem("Coupe", "Coupe", true));

            //    }

            //}

            //if (!returnList.Any())
            //    returnList.Add(createSelectListItem(bodyType, bodyType, true));


            //foreach (var bd in bodyTypeList)
            //{

            //    if (flag == true)
            //    {
            //        returnList.Add(SelectListHelper.createSelectListItem(bd.bodyTypeName,bd.bodyTypeName, true));
            //        flag = false;
            //    }
            //    else
            //        returnList.Add(SelectListHelper.createSelectListItem(bd.bodyTypeName, bd.bodyTypeName, false));

            //}

            return returnList.AsEnumerable();
        }

      

        public static IEnumerable<SelectListItem> InitalDriveTrainList(string driveTrain)
        {
            var returnList = new List<SelectListItem>();

            bool flag = true;

            var driveTrainList = new List<PostClDriveTrain>();

            if (GlobalVar.DriveTrainList == null)
            {
                 DataHelper.GetDriveTrains();
                driveTrainList.AddRange(GlobalVar.DriveTrainList);
            }
            else
            {
                driveTrainList.AddRange(GlobalVar.DriveTrainList);
            }


            if (driveTrainList.Any(x=>x.Name==driveTrain))
            {
                returnList.Add(CreateSelectListItem(driveTrainList.First(x => x.Name == driveTrain).TextValue,
                                                    driveTrainList.First(x => x.Name == driveTrain).TextValue, true));

                foreach (var driveNode in driveTrainList.Where(x=>x.Name!=driveTrain))
                {
                    returnList.Add(CreateSelectListItem(driveNode.TextValue,driveNode.TextValue, false));
                 
                }
            }
            else
            {
                foreach (var driveNode in driveTrainList)
                {
                    if (flag)
                    {
                        returnList.Add(CreateSelectListItem(driveNode.TextValue,
                                                            driveNode.TextValue, true));
                        flag = false;
                    }
                    else
                        returnList.Add(CreateSelectListItem(driveNode.TextValue,
                                                            driveNode.TextValue, false));
                }
            }


            return returnList.AsEnumerable();
        }

        public static IEnumerable<SelectListItem> InitalWarrantyList()
        {
            var returnList = new List<SelectListItem>
                {
                    CreateSelectListItem("As Is (Default)", "None", true),
                    CreateSelectListItem("Limited", "Limited", false),
                    CreateSelectListItem("Full", "Full", false)
                };

            return returnList.AsEnumerable();
        }

        public static IEnumerable<SelectListItem> InitalDurationList()
        {
            var returnList = new List<SelectListItem>
                {
                    CreateSelectListItem("Days", "Days", true),
                    CreateSelectListItem("Months", "Months", false),
                    CreateSelectListItem("Years", "Years", false)
                };

            return returnList.AsEnumerable();
        }

        public static IEnumerable<SelectListItem> InitialEditTranmmissionList(string selectedTranmission)
        {
            var returnList = new List<SelectListItem>();

            bool flag = true;

            if (String.IsNullOrEmpty(selectedTranmission))
            {
                returnList.Add(CreateSelectListItem("Automatic", "Automatic", true));
                returnList.Add(CreateSelectListItem("Manual", "Manual", false));
                returnList.Add(CreateSelectListItem("Shiftable Automatic Transmission",
                                                    "Shiftable Automatic Transmission", false));
            }
            else
            {
                if (selectedTranmission.Equals("Automatic"))
                {
                    returnList.Add(CreateSelectListItem("Automatic", "Automatic", true));
                    returnList.Add(CreateSelectListItem("Manual", "Manual", false));
                    returnList.Add(CreateSelectListItem("Shiftable Automatic Transmission",
                                                        "Shiftable Automatic Transmission", false));
                }
                else if (selectedTranmission.Equals("Manual"))
                {
                    returnList.Add(CreateSelectListItem("Manual", "Manual", true));
                    returnList.Add(CreateSelectListItem("Automatic", "Automatic", false));
                    returnList.Add(CreateSelectListItem("Shiftable Automatic Transmission",
                                                        "Shiftable Automatic Transmission", false));
                }

                else if (selectedTranmission.Equals("Shiftable Automatic Transmission"))
                {
                    returnList.Add(CreateSelectListItem("Shiftable Automatic Transmission",
                                                        "Shiftable Automatic Transmission", true));
                    returnList.Add(CreateSelectListItem("Automatic", "Automatic", false));
                    returnList.Add(CreateSelectListItem("Manual", "Manual", false));
                }
                else
                {
                    returnList.Add(CreateSelectListItem("Automatic", "Automatic", true));
                    returnList.Add(CreateSelectListItem("Manual", "Manual", false));
                    returnList.Add(CreateSelectListItem("Shiftable Automatic Transmission",
                                                        "Shiftable Automatic Transmission", false));
                }
            }

            return returnList.AsEnumerable();
        }

        public static IEnumerable<SelectListItem> InitalEditDriveTrainList(string wheelDrive)
        {
            var returnList = new List<SelectListItem>();

            XmlNode driveNode = XMLHelper.SelectOneElement("Drive",
                /*System.Web.HttpContext.Current.Server.MapPath("~/App_Data/WheelDrive.xml")*/
                                                           "", "Value=" + wheelDrive);

            XmlNodeList allNode = XMLHelper.SelectElements("Drive",
                /*System.Web.HttpContext.Current.Server.MapPath("~/App_Data/WheelDrive.xml")*/
                                                           "");

            bool flag = true;

            if (driveNode != null)
            {
                returnList.Add(CreateSelectListItem(driveNode.Attributes["Value"].Value,
                                                    driveNode.Attributes["Value"].Value, true));

                foreach (XmlNode node in allNode)
                {
                    if (!node.Attributes["Value"].Value.Equals(driveNode.Attributes["Value"].Value))
                        returnList.Add(CreateSelectListItem(node.Attributes["Value"].Value,
                                                            node.Attributes["Value"].Value, false));
                }
            }
            else
            {
                foreach (XmlNode node in allNode)
                {
                    if (flag)
                    {
                        returnList.Add(CreateSelectListItem(node.Attributes["Value"].Value,
                                                            node.Attributes["Value"].Value, true));

                        flag = false;
                    }
                    else
                    {
                        returnList.Add(CreateSelectListItem(node.Attributes["Value"].Value,
                                                            node.Attributes["Value"].Value, false));
                    }
                }
            }
            returnList.Add(CreateSelectListItem("Other Drives", "Other Drives", false));

            return returnList.AsEnumerable();
        }

        public static IEnumerable<SelectDetailListItem> InitalFactoryPackagesOrOption(List<ExtendedFactoryOptions> list)
        {
            var returnList = new List<SelectDetailListItem>();
            if (list != null && list.Any())
            {
                foreach (ExtendedFactoryOptions package in list)
                {
                    returnList.Add(CreateSelectListItem(package.GetName() + " " + package.GetMsrp(),
                                                        package.GetName() + package.GetMsrp(), package.Description,
                                                        false));
                }
            }
            return returnList.AsEnumerable();
        }

        public static IEnumerable<SelectListItem> InitalEbayExteriorColorList()
        {
            var returnList = new List<SelectListItem>
                {
                    CreateSelectListItem("Black", "2288", true),
                    CreateSelectListItem("Blue", "2289", false),
                    CreateSelectListItem("Brown", "2290", false),
                    CreateSelectListItem("Burgundy", "10409", false),
                    CreateSelectListItem("Gold", "10407", false),
                    CreateSelectListItem("Gray", "2291", false),
                    CreateSelectListItem("Green", "2196", false),
                    CreateSelectListItem("Orange", "22710", false),
                    CreateSelectListItem("Purple", "10419", false),
                    CreateSelectListItem("Red", "2375", false),
                    CreateSelectListItem("Silver", "10421", false),
                    CreateSelectListItem("Tan", "2287", false),
                    CreateSelectListItem("Teal", "10422", false),
                    CreateSelectListItem("White", "2317", false),
                    CreateSelectListItem("Yellow", "2376", false),
                    CreateSelectListItem("Other Colors", "-12", false)
                };


            return returnList.AsEnumerable();
        }

        public static IEnumerable<SelectListItem> InitalEbayInteriorColorList()
        {
            var returnList = new List<SelectListItem>
                {
                    CreateSelectListItem("Black", "2288", true),
                    CreateSelectListItem("Blue", "2289", false),
                    CreateSelectListItem("Brown", "2290", false),
                    CreateSelectListItem("Burgundy", "10409", false),
                    CreateSelectListItem("Gold", "10407", false),
                    CreateSelectListItem("Gray", "2291", false),
                    CreateSelectListItem("Green", "2196", false),
                    CreateSelectListItem("Red", "2375", false),
                    CreateSelectListItem("Tan", "2287", false),
                    CreateSelectListItem("Teal", "10422", false),
                    CreateSelectListItem("White", "2317", false),
                    CreateSelectListItem("Other Colors", "-12", false)
                };


            return returnList.AsEnumerable();
        }

        public static IEnumerable<SelectListItem> InitalAuctionTypeList()
        {
            var returnList = new List<SelectListItem>
                {
                    CreateSelectListItem("Buy It Now Only", "FixedPriceItem", true),
                    CreateSelectListItem("Buy It Now / Best Offer", "BuyItNowBestOffer", false),
                    CreateSelectListItem("Standard Auction Listing", "Chinese", false),
                    CreateSelectListItem("Standard Auction w/o Buy It Now", "ChineseNoBuyItNow", false)
                };

            return returnList.AsEnumerable();
        }

        public static IEnumerable<SelectListItem> InitalGalleryList()
        {
            var returnList = new List<SelectListItem>
                {
                    CreateSelectListItem("Gallery", "Gallery", true),
                    CreateSelectListItem("Gallery Featured $99.95", "featured", false)
                };


            return returnList.AsEnumerable();
        }

        public static IEnumerable<SelectListItem> InitalHoursList()
        {
            var returnList = new List<SelectListItem>
                {
                    CreateSelectListItem("24", "24", true),
                    CreateSelectListItem("48", "48", false),
                    CreateSelectListItem("72", "72", false)
                };


            return returnList.AsEnumerable();
        }

        public static IEnumerable<SelectListItem> InitalAuctionLength()
        {
            var returnList = new List<SelectListItem>
                {
                    CreateSelectListItem(" 10 days $15.00", "Days_10", true),
                    CreateSelectListItem(" 21 days $32.00", "Days_21", false),
                    CreateSelectListItem(" 3 days", "Days_3", false),
                    CreateSelectListItem(" 5 days", "Days_5", false),
                    CreateSelectListItem(" 7 days", "Days_7", false)
                };


            return returnList.AsEnumerable();
        }

        public static IEnumerable<SelectListItem> InitalLanguagesList()
        {
            var returnList = new List<SelectListItem>
                {
                    CreateSelectListItem("English", "1", true),
                    CreateSelectListItem("Spanish", "2", false),
                    CreateSelectListItem("English/Spanish", "3", false)
                };

            return returnList.AsEnumerable();
        }

        public static IEnumerable<SelectListItem> InitalTruckTypeList()
        {
            var returnList = new List<SelectListItem>
                {
                    CreateSelectListItem("Truck", "Truck", true),
                    CreateSelectListItem("Truck Body", "Truck Body", false),
                    CreateSelectListItem("Trailer", "Trailer", false)
                };


            return returnList.AsEnumerable();
        }

        public static IEnumerable<SelectListItem> InitalTruckCategoryList(List<string> truckCategoryList)
        {
            var returnList = new List<SelectListItem>();

            bool flag = true;

            foreach (string tmp in truckCategoryList)
            {
                if (flag)
                {
                    returnList.Add(CreateSelectListItem(tmp, tmp, true));
                    flag = false;
                }
                else
                    returnList.Add(CreateSelectListItem(tmp, tmp, false));
            }

            return returnList.AsEnumerable();
        }

        public static IEnumerable<SelectListItem> InitalVehicleTypeList()
        {
            var returnList = new List<SelectListItem>();

            returnList.Add(CreateSelectListItem("Car", "Car", true));

            returnList.Add(CreateSelectListItem("Truck", "Truck", false));

            return returnList.AsEnumerable();
        }

        public static IEnumerable<SelectListItem> InitalVehicleTypeListForTruck()
        {
            var returnList = new List<SelectListItem>();

            returnList.Add(CreateSelectListItem("Truck", "Truck", true));

            returnList.Add(CreateSelectListItem("Car", "Car", false));

            return returnList.AsEnumerable();
        }

        public static IEnumerable<SelectListItem> InitalTruckClassList()
        {
            var returnList = new List<SelectListItem>();

            returnList.Add(CreateSelectListItem("GVW 0 - 6000 Pounds", "Class 1", true));

            returnList.Add(CreateSelectListItem("GVW 6001 - 10000 Pounds", "Class 2", false));

            returnList.Add(CreateSelectListItem("GVW 10001 - 14000 Pounds", "Class 3", false));

            returnList.Add(CreateSelectListItem("GVW 14001 - 16000 Pounds", "Class 4", false));

            returnList.Add(CreateSelectListItem("GVW 16001 - 19500 Pounds", "Class 5", false));

            returnList.Add(CreateSelectListItem("GVW 19501 - 26000 Pounds", "Class 6", false));

            returnList.Add(CreateSelectListItem("GVW 26001 - 33001 Pounds", "Class 7", false));

            returnList.Add(CreateSelectListItem("GVW 33001 - 150000 Pounds", "Class 8", false));


            return returnList.AsEnumerable();
        }

        public static IEnumerable<SelectListItem> InitalTruckClassList(string truckClass)
        {
            var returnList = new List<SelectListItem>();
            if (String.IsNullOrEmpty(truckClass))
                return InitalTruckClassList();
            else
            {
                switch (truckClass)
                {
                    case "Class 1":
                        returnList.Add(CreateSelectListItem("GVW 0 - 6000 Pounds", "Class 1", true));

                        returnList.Add(CreateSelectListItem("GVW 6001 - 10000 Pounds", "Class 2", false));

                        returnList.Add(CreateSelectListItem("GVW 10001 - 14000 Pounds", "Class 3", false));

                        returnList.Add(CreateSelectListItem("GVW 14001 - 16000 Pounds", "Class 4", false));

                        returnList.Add(CreateSelectListItem("GVW 16001 - 19500 Pounds", "Class 5", false));

                        returnList.Add(CreateSelectListItem("GVW 19501 - 26000 Pounds", "Class 6", false));

                        returnList.Add(CreateSelectListItem("GVW 26001 - 33001 Pounds", "Class 7", false));

                        returnList.Add(CreateSelectListItem("GVW 33001 - 150000 Pounds", "Class 8", false));
                        break;
                    case "Class 2":

                        returnList.Add(CreateSelectListItem("GVW 6001 - 10000 Pounds", "Class 2", true));
                        returnList.Add(CreateSelectListItem("GVW 0 - 6000 Pounds", "Class 1", false));


                        returnList.Add(CreateSelectListItem("GVW 10001 - 14000 Pounds", "Class 3", false));

                        returnList.Add(CreateSelectListItem("GVW 14001 - 16000 Pounds", "Class 4", false));

                        returnList.Add(CreateSelectListItem("GVW 16001 - 19500 Pounds", "Class 5", false));

                        returnList.Add(CreateSelectListItem("GVW 19501 - 26000 Pounds", "Class 6", false));

                        returnList.Add(CreateSelectListItem("GVW 26001 - 33001 Pounds", "Class 7", false));

                        returnList.Add(CreateSelectListItem("GVW 33001 - 150000 Pounds", "Class 8", false));
                        break;
                    case "Class 3":

                        returnList.Add(CreateSelectListItem("GVW 10001 - 14000 Pounds", "Class 3", true));

                        returnList.Add(CreateSelectListItem("GVW 0 - 6000 Pounds", "Class 1", false));

                        returnList.Add(CreateSelectListItem("GVW 6001 - 10000 Pounds", "Class 2", false));


                        returnList.Add(CreateSelectListItem("GVW 14001 - 16000 Pounds", "Class 4", false));

                        returnList.Add(CreateSelectListItem("GVW 16001 - 19500 Pounds", "Class 5", false));

                        returnList.Add(CreateSelectListItem("GVW 19501 - 26000 Pounds", "Class 6", false));

                        returnList.Add(CreateSelectListItem("GVW 26001 - 33001 Pounds", "Class 7", false));

                        returnList.Add(CreateSelectListItem("GVW 33001 - 150000 Pounds", "Class 8", false));
                        break;
                    case "Class 4":


                        returnList.Add(CreateSelectListItem("GVW 14001 - 16000 Pounds", "Class 4", true));
                        returnList.Add(CreateSelectListItem("GVW 0 - 6000 Pounds", "Class 1", false));

                        returnList.Add(CreateSelectListItem("GVW 6001 - 10000 Pounds", "Class 2", false));

                        returnList.Add(CreateSelectListItem("GVW 10001 - 14000 Pounds", "Class 3", false));


                        returnList.Add(CreateSelectListItem("GVW 16001 - 19500 Pounds", "Class 5", false));

                        returnList.Add(CreateSelectListItem("GVW 19501 - 26000 Pounds", "Class 6", false));

                        returnList.Add(CreateSelectListItem("GVW 26001 - 33001 Pounds", "Class 7", false));

                        returnList.Add(CreateSelectListItem("GVW 33001 - 150000 Pounds", "Class 8", false));
                        break;
                    case "Class 5":

                        returnList.Add(CreateSelectListItem("GVW 16001 - 19500 Pounds", "Class 5", true));


                        returnList.Add(CreateSelectListItem("GVW 0 - 6000 Pounds", "Class 1", false));

                        returnList.Add(CreateSelectListItem("GVW 6001 - 10000 Pounds", "Class 2", false));

                        returnList.Add(CreateSelectListItem("GVW 10001 - 14000 Pounds", "Class 3", false));

                        returnList.Add(CreateSelectListItem("GVW 14001 - 16000 Pounds", "Class 4", false));


                        returnList.Add(CreateSelectListItem("GVW 19501 - 26000 Pounds", "Class 6", false));

                        returnList.Add(CreateSelectListItem("GVW 26001 - 33001 Pounds", "Class 7", false));

                        returnList.Add(CreateSelectListItem("GVW 33001 - 150000 Pounds", "Class 8", false));
                        break;
                    case "Class 6":

                        returnList.Add(CreateSelectListItem("GVW 19501 - 26000 Pounds", "Class 6", true));

                        returnList.Add(CreateSelectListItem("GVW 0 - 6000 Pounds", "Class 1", false));

                        returnList.Add(CreateSelectListItem("GVW 6001 - 10000 Pounds", "Class 2", false));

                        returnList.Add(CreateSelectListItem("GVW 10001 - 14000 Pounds", "Class 3", false));

                        returnList.Add(CreateSelectListItem("GVW 14001 - 16000 Pounds", "Class 4", false));

                        returnList.Add(CreateSelectListItem("GVW 16001 - 19500 Pounds", "Class 5", false));


                        returnList.Add(CreateSelectListItem("GVW 26001 - 33001 Pounds", "Class 7", false));

                        returnList.Add(CreateSelectListItem("GVW 33001 - 150000 Pounds", "Class 8", false));
                        break;
                    case "Class 7":

                        returnList.Add(CreateSelectListItem("GVW 26001 - 33001 Pounds", "Class 7", true));


                        returnList.Add(CreateSelectListItem("GVW 0 - 6000 Pounds", "Class 1", false));

                        returnList.Add(CreateSelectListItem("GVW 6001 - 10000 Pounds", "Class 2", false));

                        returnList.Add(CreateSelectListItem("GVW 10001 - 14000 Pounds", "Class 3", false));

                        returnList.Add(CreateSelectListItem("GVW 14001 - 16000 Pounds", "Class 4", false));

                        returnList.Add(CreateSelectListItem("GVW 16001 - 19500 Pounds", "Class 5", false));

                        returnList.Add(CreateSelectListItem("GVW 19501 - 26000 Pounds", "Class 6", false));


                        returnList.Add(CreateSelectListItem("GVW 33001 - 150000 Pounds", "Class 8", false));
                        break;
                    case "Class 8":
                        returnList.Add(CreateSelectListItem("GVW 33001 - 150000 Pounds", "Class 8", true));

                        returnList.Add(CreateSelectListItem("GVW 0 - 6000 Pounds", "Class 1", false));

                        returnList.Add(CreateSelectListItem("GVW 6001 - 10000 Pounds", "Class 2", false));

                        returnList.Add(CreateSelectListItem("GVW 10001 - 14000 Pounds", "Class 3", false));

                        returnList.Add(CreateSelectListItem("GVW 14001 - 16000 Pounds", "Class 4", false));

                        returnList.Add(CreateSelectListItem("GVW 16001 - 19500 Pounds", "Class 5", false));

                        returnList.Add(CreateSelectListItem("GVW 19501 - 26000 Pounds", "Class 6", false));

                        returnList.Add(CreateSelectListItem("GVW 26001 - 33001 Pounds", "Class 7", false));


                        break;


                    default:
                        return InitalTruckClassList();
                }
            }

            return returnList.AsEnumerable();
        }

        public static IEnumerable<SelectListItem> InitalSoldOutList(string soldAction)
        {
            var returnList = new List<SelectListItem>();

            if (soldAction.Equals(DisplayDefaultSoldOut))
            {
                returnList.Add(CreateSelectListItem(DisplayDefaultSoldOut, DisplayDefaultSoldOut, true));
                returnList.Add(CreateSelectListItem(Display3DaysSoldOut, Display3DaysSoldOut, false));
                returnList.Add(CreateSelectListItem(Display5DaysSoldOut, Display5DaysSoldOut, false));
                returnList.Add(CreateSelectListItem(Display7DaysSoldOut, Display7DaysSoldOut, false));
                returnList.Add(CreateSelectListItem(Display30DaysSoldOut, Display30DaysSoldOut, false));
            }
            else if (soldAction.Equals(Display3DaysSoldOut))
            {
                returnList.Add(CreateSelectListItem(Display3DaysSoldOut, Display3DaysSoldOut, true));
                returnList.Add(CreateSelectListItem(DisplayDefaultSoldOut, DisplayDefaultSoldOut, false));
                returnList.Add(CreateSelectListItem(Display5DaysSoldOut, Display5DaysSoldOut, false));
                returnList.Add(CreateSelectListItem(Display7DaysSoldOut, Display7DaysSoldOut, false));
                returnList.Add(CreateSelectListItem(Display30DaysSoldOut, Display30DaysSoldOut, false));
            }
            else if (soldAction.Equals(Display5DaysSoldOut))
            {
                returnList.Add(CreateSelectListItem(Display5DaysSoldOut, Display5DaysSoldOut, true));
                returnList.Add(CreateSelectListItem(DisplayDefaultSoldOut, DisplayDefaultSoldOut, false));
                returnList.Add(CreateSelectListItem(Display3DaysSoldOut, Display3DaysSoldOut, false));
                returnList.Add(CreateSelectListItem(Display7DaysSoldOut, Display7DaysSoldOut, false));
                returnList.Add(CreateSelectListItem(Display30DaysSoldOut, Display30DaysSoldOut, false));
            }
            else if (soldAction.Equals(Display7DaysSoldOut))
            {
                returnList.Add(CreateSelectListItem(Display7DaysSoldOut, Display7DaysSoldOut, true));
                returnList.Add(CreateSelectListItem(DisplayDefaultSoldOut, DisplayDefaultSoldOut, false));
                returnList.Add(CreateSelectListItem(Display3DaysSoldOut, Display3DaysSoldOut, false));
                returnList.Add(CreateSelectListItem(Display5DaysSoldOut, Display5DaysSoldOut, false));
                returnList.Add(CreateSelectListItem(Display30DaysSoldOut, Display30DaysSoldOut, false));
            }
            else
            {
                returnList.Add(CreateSelectListItem(Display30DaysSoldOut, Display30DaysSoldOut, true));
                returnList.Add(CreateSelectListItem(DisplayDefaultSoldOut, DisplayDefaultSoldOut, false));
                returnList.Add(CreateSelectListItem(Display3DaysSoldOut, Display3DaysSoldOut, false));
                returnList.Add(CreateSelectListItem(Display5DaysSoldOut, Display5DaysSoldOut, false));
                returnList.Add(CreateSelectListItem(Display7DaysSoldOut, Display7DaysSoldOut, false));
            }


            return returnList.AsEnumerable();
        }

        public static IEnumerable<SelectListItem> InitalSortSetList()
        {
            var returnList = new List<SelectListItem>();

            returnList.Add(CreateSelectListItem("Select an option...", "Select an option...", false));
            returnList.Add(CreateSelectListItem("Make", "Make", false));
            returnList.Add(CreateSelectListItem("Model", "Model", false));
            returnList.Add(CreateSelectListItem("Year", "Year", false));


            return returnList.AsEnumerable();
        }

        public static IEnumerable<SelectListItem> InitalSortSetList(string sortSet)
        {
            //builder.Append("<option selected=\"selected\" value=\"Make\">Make (Default)</option>");
            //builder.Append("<option value=\"Model\">Model</option>");
            //builder.Append("<option value=\"Year\">Year</option>");
            //builder.Append("<option value=\"Market\">Market</option>");
            var returnList = new List<SelectListItem>();

            if (sortSet.Equals("Make"))
            {
                returnList.Add(CreateSelectListItem("Make(Default)", "Make", true));
                returnList.Add(CreateSelectListItem("Model", "Model", false));
                returnList.Add(CreateSelectListItem("Year", "Year", false));
                returnList.Add(CreateSelectListItem("Age", "Age", false));
                returnList.Add(CreateSelectListItem("Market", "Market", false));
            }
            else if (sortSet.Equals("Model"))
            {
                returnList.Add(CreateSelectListItem("Model(Default)", "Model", true));
                returnList.Add(CreateSelectListItem("Make", "Make", false));
                returnList.Add(CreateSelectListItem("Year", "Year", false));
                returnList.Add(CreateSelectListItem("Age", "Age", false));
                returnList.Add(CreateSelectListItem("Market", "Market", false));
            }
            else if (sortSet.Equals("Year"))
            {
                returnList.Add(CreateSelectListItem("Year(Default)", "Year", true));
                returnList.Add(CreateSelectListItem("Make", "Make", false));
                returnList.Add(CreateSelectListItem("Model", "Model", false));
                returnList.Add(CreateSelectListItem("Age", "Age", false));
                returnList.Add(CreateSelectListItem("Market", "Market", false));
            }
            else if (sortSet.Equals("Market"))
            {
                returnList.Add(CreateSelectListItem("Market(Default)", "Market", true));
                returnList.Add(CreateSelectListItem("Make", "Make", false));
                returnList.Add(CreateSelectListItem("Model", "Model", false));
                returnList.Add(CreateSelectListItem("Age", "Age", false));
                returnList.Add(CreateSelectListItem("Year", "Year", false));
            }
            else if (sortSet.Equals("Age"))
            {
                returnList.Add(CreateSelectListItem("Age(Default)", "Age", true));
                returnList.Add(CreateSelectListItem("Make", "Make", false));
                returnList.Add(CreateSelectListItem("Model", "Model", false));
                returnList.Add(CreateSelectListItem("Market", "Market", false));
                returnList.Add(CreateSelectListItem("Year", "Year", false));
            }

            return returnList.AsEnumerable();
        }

        public static SelectListItem CreateSelectListItem(string text, string value, bool selected)
        {
            var item = new SelectListItem
                {
                    Text = text,
                    Value = value,
                    Selected = selected
                };

            return item;
        }

        public static SelectDetailListItem CreateSelectListItem(string text, string value, string fullDescription,
                                                                bool selected)
        {
            var item = new SelectDetailListItem
                {
                    Text = text,
                    Value = value,
                    Selected = selected,
                    Description = fullDescription
                };

            return item;
        }

        public static IEnumerable<SelectListItem> InitialCountryList()
        {
            XmlNode stateListNode = XMLHelper.SelectFirstElement("CountryList",
                /*HttpContext.Current.Server.MapPath("~/App_Data/CountryList.xml")*/
                                                                 "");


            List<SelectListItem> returnList = (from XmlNode stateNode in stateListNode.ChildNodes
                                               select stateNode.LastChild.InnerText.Equals("US")
                                                          ? CreateSelectListItem(stateNode.FirstChild.InnerText,
                                                                                 stateNode.LastChild.InnerText, true)
                                                          : CreateSelectListItem(stateNode.FirstChild.InnerText,
                                                                                 stateNode.LastChild.InnerText, false))
                .ToList();

            return returnList.AsEnumerable();
        }
    }
}