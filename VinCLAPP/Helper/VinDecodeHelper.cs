using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using VinCLAPP.AutomativeDescriptionService7;
using VinCLAPP.Model;

namespace VinCLAPP.Helper
{
    public class VinDecodeHelper
    {
        public static AppraisalViewFormModel DecodeProcessingByVin(string vin)
        {
            var viewModel = new AppraisalViewFormModel();

            var autoService = new ChromeAutoService();

            VehicleDescription vehicleInfo = autoService.GetVehicleInformationFromVin(vin);

            if (vehicleInfo != null &&
                (vehicleInfo.responseStatus.responseCode == ResponseStatusResponseCode.Successful ||
                 vehicleInfo.responseStatus.responseCode == ResponseStatusResponseCode.ConditionallySuccessful))
            {
                if (vehicleInfo.style != null && vehicleInfo.style.Any())
                {
                    Style firstStyle = vehicleInfo.style.FirstOrDefault();
                    if (firstStyle != null)
                    {
                        bool existed;
                        viewModel.TrimList = SelectListHelper.InitalTrimList(viewModel, firstStyle.trim,
                                                                             vehicleInfo.style, firstStyle.id,
                                                                             out existed);
                        //SessionHandler.ChromeTrimList = viewModel.TrimList;
                        vehicleInfo = autoService.GetVehicleInformationFromVin(vin, firstStyle.id);
                        VehicleDescription styleInfo = autoService.GetStyleInformationFromStyleId(firstStyle.id);

                        viewModel = GetVehicleInfoFromChromeDecodeWithStyle(vehicleInfo, styleInfo);
                    }
                }
            }

            if (viewModel.IsTruck)
            {
                viewModel.TruckTypeList = SelectListHelper.InitalTruckTypeList();

                //viewModel.TruckCategoryList = SelectListHelper.InitalTruckCategoryList(SQLHelper.GetListOfTruckCategoryByTruckType(viewModel.TruckTypeList.First().Value));

                viewModel.TruckClassList = SelectListHelper.InitalTruckClassList();
            }

            return viewModel;
        }

        public static AppraisalViewFormModel GetVehicleInformationFromStyleId(string styleId)
        {
            var autoService = new ChromeAutoService();
            var styleInfo = autoService.GetStyleInformationFromStyleId(Convert.ToInt32(styleId));
            var appraisal = GetVehicleInfoFromChromeDecode(styleInfo);
            return appraisal;
        }

        public static AppraisalViewFormModel GetVehicleInformationFromYearMakeModel(int year, string make, string model)
        {
            var autoService = new ChromeAutoService();
            var styleInfo = autoService.GetVehicleInformationFromYearMakeModel(year, make,model);
            var appraisal = GetVehicleInfoFromChromeDecode(styleInfo);
            return appraisal;
        }

        public static AppraisalViewFormModel GetVehicleInformationFromStyleId(string vin, string styleId)
        {
            var autoService = new ChromeAutoService();
            var vehicleInfo = autoService.GetVehicleInformationFromVin(vin, Convert.ToInt32(styleId));
            var styleInfo = autoService.GetStyleInformationFromStyleId(Convert.ToInt32(styleId));
            return GetVehicleInfoFromChromeDecodeWithStyle(vehicleInfo, styleInfo);
        }

        public static AppraisalViewFormModel GetVehicleInfoFromChromeDecode(VehicleDescription vehicleInfo)
        {
            if (vehicleInfo == null) return null;

            var appraisal = new AppraisalViewFormModel
                {
                    VinDecodeSuccess = true,
                    AppraisalDate = DateTime.Now.ToShortDateString(),
                    VinNumber = vehicleInfo.vinDescription != null ? vehicleInfo.vinDescription.vin : string.Empty,
                    AppraisalModel = vehicleInfo.bestModelName,
                    Make = vehicleInfo.bestMakeName,
                    Trim = vehicleInfo.bestTrimName,
                    SelectedModel = vehicleInfo.bestModelName,
                    //TODO: get Chrom Model ID
                    ModelYear = vehicleInfo.modelYear.ToString(),
                    ExteriorColorList = SelectListHelper.InitalExteriorColorList(vehicleInfo.exteriorColor),
                    InteriorColorList = SelectListHelper.InitalInteriorColorList(vehicleInfo.interiorColor)
                };

            if (vehicleInfo.style != null && vehicleInfo.style.Any())
            {
                Style firstStyle = vehicleInfo.style.FirstOrDefault();
                if (firstStyle != null)
                {
                    appraisal.Door = firstStyle.passDoors.ToString();
                    appraisal.MSRP = firstStyle.basePrice.msrp.ToString("C");
                    appraisal.DriveTrainList = SelectListHelper.InitalDriveTrainList(firstStyle.drivetrain.ToString());
                    bool existed;
                    appraisal.TrimList = SelectListHelper.InitalTrimList(appraisal, firstStyle.trim, vehicleInfo.style,
                                                                         firstStyle.id, out existed);
                    if (firstStyle.stockImage != null)
                        appraisal.DefaultImageUrl = firstStyle.stockImage.url;
                }
            }

            var chromeAutoService = new ChromeAutoService();
            List<ExtendedFactoryOptions> listPackageOptions = chromeAutoService.GetPackageOptions(vehicleInfo);
            List<ExtendedFactoryOptions> listNonInstalledOptions = chromeAutoService.GetNonInstalledOptions(vehicleInfo);

            var builder = new StringBuilder();

            if (vehicleInfo.standard != null && vehicleInfo.standard.Any())
            {
                foreach (Standard fo in vehicleInfo.standard)
                {
                    builder.Append(fo.description + ",");
                }

                if (!String.IsNullOrEmpty(builder.ToString()))
                    builder.Remove(builder.Length - 1, 1);

                appraisal.StandardInstalledOption = builder.ToString();
            }

            appraisal.FactoryPackageOptions = SelectListHelper.InitalFactoryPackagesOrOption(listPackageOptions);
            appraisal.FactoryNonInstalledOptions =
                SelectListHelper.InitalFactoryPackagesOrOption(listNonInstalledOptions);

            if (vehicleInfo.vinDescription != null && !String.IsNullOrEmpty(vehicleInfo.vinDescription.bodyType))
            {
                appraisal.BodyTypeList = SelectListHelper.InitialBodyTypeList(vehicleInfo.vinDescription.bodyType);
            }
            else
            {
                StyleBodyType[] bodyType = vehicleInfo.style.Last().bodyType;
                appraisal.BodyTypeList =
                    SelectListHelper.InitialBodyTypeList(vehicleInfo.style != null
                                                             ? (bodyType != null
                                                                    ? bodyType.Last().Value
                                                                    : vehicleInfo.bestStyleName)
                                                             : vehicleInfo.bestStyleName);
                if (appraisal.CylinderList == null)
                {
                    appraisal.CylinderList = new BindingList<SelectListItem>();
                }

                if (appraisal.LitersList == null)
                {
                    appraisal.LitersList = new BindingList<SelectListItem>();
                }

                if (appraisal.FuelList == null)
                {
                    appraisal.FuelList = new BindingList<SelectListItem>();
                }
            }

            if (vehicleInfo.engine != null)
            {
                appraisal.FuelList = SelectListHelper.InitialFuelList(vehicleInfo.engine);
                appraisal.CylinderList = SelectListHelper.InitialCylinderList(vehicleInfo.engine);
                appraisal.LitersList = SelectListHelper.InitialLitterList(vehicleInfo.engine);

                Engine firstEngine = vehicleInfo.engine.FirstOrDefault();
                if (firstEngine != null && firstEngine.fuelEconomy != null)
                {
                    appraisal.FuelEconomyCity = firstEngine.fuelEconomy.city.low.ToString();
                    appraisal.FuelEconomyHighWay = firstEngine.fuelEconomy.hwy.low.ToString();
                }
            }

            if (vehicleInfo.vinDescription != null && vehicleInfo.vinDescription.marketClass != null)
            {
                if (
                    vehicleInfo.vinDescription.marketClass.Any(
                        tmp => tmp.Value.Contains("Truck") || tmp.Value.Contains("Cargo Vans")))
                {
                    appraisal.IsTruck = true;
                }
            }

            return appraisal;
        }

        public static AppraisalViewFormModel GetVehicleInfoFromChromeDecodeWithStyle(VehicleDescription vehicleInfo,
                                                                                     VehicleDescription styleInfo)
        {
            AppraisalViewFormModel car = GetVehicleInfoFromChromeDecode(vehicleInfo);

            if (styleInfo != null && car != null)
            {
                
                bool existed;
                car.TrimList = SelectListHelper.InitalTrimList(car, styleInfo.style[0].trim, vehicleInfo.style,
                                                               styleInfo.style[0].id, out existed);
            
                var chromeAutoService = new ChromeAutoService();
                List<ExtendedFactoryOptions> listPackageOptions = chromeAutoService.GetPackageOptions(styleInfo);
                List<ExtendedFactoryOptions> listNonInstalledOptions =
                    chromeAutoService.GetNonInstalledOptions(styleInfo);

            }

            return car;
        }

        public static List<SelectListItem> GetMakeList(string modelYear)
        {
            var autoService = new ChromeAutoService();

            var makeList = autoService.GetDivisions(Convert.ToInt32(modelYear)).ToList();

            //if (GlobalVar.LuxuryMakesList == null)
            //{
            //    DataHelper.GetLuxuryMakes();

            //    makeList.AddRange(GlobalVar.LuxuryMakesList);
            //}
            //else
            //{
            //    makeList.AddRange(GlobalVar.LuxuryMakesList);
            //}

            return makeList.OrderBy(x => x.Value).Select(i => new SelectListItem() { Text = i.Value, Value = i.id.ToString(CultureInfo.InvariantCulture) }).ToList();
        }

        private static XmlNodeList SelectElements(string pathToElement, string pathToFile)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(pathToFile);
                String XPathExpression = "//" + pathToElement;
                XmlNodeList nodelist = doc.SelectNodes(XPathExpression);
                if (nodelist.Count > 0)
                    return nodelist;
                return null;

            }
            catch (System.Exception ex)
            {
                throw ex;

            }
        }

        public static List<SelectListItem> GetModelList(string modelYear, string makeId)
        {
            var year = Convert.ToInt32(modelYear);
            var id = Convert.ToInt32(makeId);
            if (id <= 0) { return null; }
            var autoService = new ChromeAutoService();
            var source = autoService.GetModelsByDivision(year, id);
            if (source == null) return null;
            return source.Select(i => new SelectListItem() { Text = i.Value, Value = i.id.ToString() }).ToList();
        }

        public static List<SelectListItem> GetTrimList(string modelId)
        {
            var list = new List<SelectListItem>();
            var id = Convert.ToInt32(modelId);

            if (id <= 0) { return null; }
            var autoService = new ChromeAutoService();

            var styles = autoService.GetStyles(id);

            var hash = new HashSet<string>();

            if (styles != null && styles.Any())
            {
                foreach (var s in styles)
                {
                    var uniqueTrim = String.IsNullOrEmpty(s.Value) ? Constants.OtherTrims : s.Value;

                    if (!hash.Contains(uniqueTrim))
                    {
                        list.Add(new SelectListItem() { Value = s.id.ToString(), Text = s.Value });
                    }

                    hash.Add(uniqueTrim);
                }
            }

            var item = list.FirstOrDefault(i => i.Text.Equals(Constants.OtherTrims));
            if (item == null && list != null && list.Count > 0)
            {
                list.Add(new SelectListItem() { Value = list[0].Value, Text = Constants.OtherTrims });
            }

            return list;
        }
    }
}