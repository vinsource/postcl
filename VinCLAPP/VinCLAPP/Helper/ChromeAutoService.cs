using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using VinCLAPP.AutomativeDescriptionService7;
using VinCLAPP.Model;

namespace VinCLAPP.Helper
{
    public class ChromeAutoService
    {
        private readonly AccountInfo _mAi;

        private readonly Description7aPortTypeClient _vinService = new Description7aPortTypeClient();

        public ChromeAutoService()
        {
            _mAi = new AccountInfo
                {
                    number = ConfigurationManager.AppSettings["accountNumber"],
                    secret = ConfigurationManager.AppSettings["accountSecret"],
                    country = ConfigurationManager.AppSettings["country"],
                    language = ConfigurationManager.AppSettings["language"]
                };
        }

        public AccountInfo GetAccountInfo()
        {
            return _mAi;
        }

        public int[] GetModelYears()
        {
            var baseReuqest = new BaseRequest { accountInfo = GetAccountInfo() };

            ModelYears temp = _vinService.getModelYears(baseReuqest);

            if (temp != null && temp.modelYear.Any())
                return temp.modelYear;

            return null;
        }

        public IdentifiedString[] GetDivisions(int mModelYear)
        {
            var mDr = new DivisionsRequest { accountInfo = GetAccountInfo(), modelYear = mModelYear };

            Divisions temp = _vinService.getDivisions(mDr);

            if (temp != null && temp.division.Any())
                return temp.division;

            return null;
        }

        public IdentifiedString[] GetSubdivisions(int mModelYear)
        {
            var mSdr = new SubdivisionsRequest { accountInfo = GetAccountInfo(), modelYear = mModelYear };

            Subdivisions temp = _vinService.getSubdivisions(mSdr);

            if (temp != null && temp.subdivision.Any())
                return temp.subdivision;

            return null;
        }

        public IdentifiedString[] GetModelsByDivision(int mModelYear, int mDivisionId)
        {
            var mMbdr = new ModelsRequest
                {
                    accountInfo = GetAccountInfo(),
                    modelYear = mModelYear,
                    Item = mDivisionId
                };

            Models temp = _vinService.getModels(mMbdr);

            if (temp != null && temp.model != null && temp.model.Any())
                return temp.model;

            return null;
        }

        public IdentifiedString[] GetModelsByDivision(int mModelYear, string mDivisionName)
        {
            var mMbdr = new ModelsRequest { accountInfo = GetAccountInfo(), modelYear = mModelYear };

            bool flag = false;

            foreach (IdentifiedString dv in GetDivisions(mModelYear).Where(dv => dv.Value.Equals(mDivisionName)))
            {
                mMbdr.Item = dv.id;
                flag = true;
                break;
            }

            if (flag)
            {
                Models temp = _vinService.getModels(mMbdr);
                if (temp != null && temp.model.Any())
                    return temp.model;

                return null;
            }

            return null;
        }

        public IdentifiedString[] GetModelsBySubDivision(int mSubdivisionId)
        {
            var mMbsdr = new ModelsRequest { accountInfo = GetAccountInfo(), Item = mSubdivisionId };

            Models temp = _vinService.getModels(mMbsdr);
            if (temp != null && temp.model.Any())
                return temp.model;

            return null;
        }

        public IdentifiedString[] GetStyles(int mModelId)
        {
            var mSr = new StylesRequest { accountInfo = GetAccountInfo(), modelId = mModelId };

            Styles temp = _vinService.getStyles(mSr);
            if (temp != null && temp.style.Any())
                return temp.style;

            return null;
        }

        public VehicleDescription GetVehicleInformationFromStyleId(int styleId)
        {
            var mSifsir = new VehicleDescriptionRequest
                {
                    accountInfo = GetAccountInfo(),
                    Items = new object[] { styleId },
                    ItemsElementName = new[] { ItemsChoiceType.styleId },
                    @switch =
                        new[]
                            {
                                Switch.DisableSafeStandards, Switch.IncludeDefinitions, Switch.IncludeRegionalVehicles,
                                Switch.ShowAvailableEquipment, Switch.ShowConsumerInformation,
                                Switch.ShowExtendedDescriptions, Switch.ShowExtendedTechnicalSpecifications
                            }
                };

            VehicleDescription temp = _vinService.describeVehicle(mSifsir);

            if (temp != null)
            {
                return temp.responseStatus.responseCode.Equals(ResponseStatusResponseCode.Successful) ? temp : null;
            }

            return null;
        }

        public VehicleDescription GetVehicleInformationFromVin(string mVin)
        {
            var mSifsir = new VehicleDescriptionRequest
                {
                    accountInfo = GetAccountInfo(),
                    Items = new object[] { mVin },
                    ItemsElementName = new[] { ItemsChoiceType.vin },
                    @switch =
                        new[]
                            {
                                Switch.DisableSafeStandards, Switch.IncludeDefinitions, Switch.IncludeRegionalVehicles,
                                Switch.ShowAvailableEquipment, Switch.ShowConsumerInformation,
                                Switch.ShowExtendedDescriptions, Switch.ShowExtendedTechnicalSpecifications
                            }
                };

            VehicleDescription temp = _vinService.describeVehicle(mSifsir);

            if (temp == null) return null;

            if (temp.responseStatus.responseCode.Equals(ResponseStatusResponseCode.Successful) && temp.modelYear > 0)
                return temp;
            return null;
        }

        public VehicleDescription GetVehicleInformationFromVin(string mVin, int reducingStyleId)
        {
            var mSifsir = new VehicleDescriptionRequest
                {
                    accountInfo = GetAccountInfo(),
                    Items = new object[] { mVin, reducingStyleId },
                    ItemsElementName = new[] { ItemsChoiceType.vin, ItemsChoiceType.reducingStyleId, },
                    @switch =
                        new[]
                            {
                                Switch.DisableSafeStandards, Switch.IncludeDefinitions, Switch.IncludeRegionalVehicles,
                                Switch.ShowAvailableEquipment, Switch.ShowConsumerInformation,
                                Switch.ShowExtendedDescriptions, Switch.ShowExtendedTechnicalSpecifications
                            }
                };

            VehicleDescription temp = _vinService.describeVehicle(mSifsir);

            if (temp == null) return null;

            return temp.responseStatus.responseCode.Equals(ResponseStatusResponseCode.Successful) ? temp : null;
        }

        public VehicleDescription GetVehicleInformationFromYearMakeModel(int mYear, string mMake, string mModel)
        {
            var mSifsir = new VehicleDescriptionRequest
                {
                    accountInfo = GetAccountInfo(),
                    Items = new object[] { mYear, mMake, mModel },
                    ItemsElementName =
                        new[] { ItemsChoiceType.modelYear, ItemsChoiceType.makeName, ItemsChoiceType.modelName, },
                    @switch =
                        new[]
                            {
                                Switch.DisableSafeStandards, Switch.IncludeDefinitions, Switch.IncludeRegionalVehicles,
                                Switch.ShowAvailableEquipment, Switch.ShowConsumerInformation,
                                Switch.ShowExtendedDescriptions, Switch.ShowExtendedTechnicalSpecifications
                            }
                };

            VehicleDescription temp = _vinService.describeVehicle(mSifsir);

            if (temp == null) return null;

            return temp.responseStatus.responseCode.Equals(ResponseStatusResponseCode.Successful) ? temp : null;
        }

        public VehicleDescription GetStyleInformationFromStyleId(int styleId)
        {
            var mSifsir = new VehicleDescriptionRequest
                {
                    accountInfo = GetAccountInfo(),
                    Items = new object[] { styleId },
                    ItemsElementName = new[] { ItemsChoiceType.styleId },
                    @switch =
                        new[]
                            {
                                Switch.DisableSafeStandards, Switch.IncludeDefinitions, Switch.IncludeRegionalVehicles,
                                Switch.ShowAvailableEquipment, Switch.ShowConsumerInformation,
                                Switch.ShowExtendedDescriptions, Switch.ShowExtendedTechnicalSpecifications
                            }
                };

            VehicleDescription temp = _vinService.describeVehicle(mSifsir);

            if (temp == null) return null;

            return temp.responseStatus.responseCode.Equals(ResponseStatusResponseCode.Successful) ? temp : null;
        }

        public bool ValidateVin(string mVin)
        {
            var mSifsir = new VehicleDescriptionRequest
                {
                    accountInfo = GetAccountInfo(),
                    Items = new object[] { mVin },
                    ItemsElementName = new[] { ItemsChoiceType.vin },
                    @switch =
                        new[]
                            {
                                Switch.DisableSafeStandards, Switch.IncludeDefinitions, Switch.IncludeRegionalVehicles,
                                Switch.ShowAvailableEquipment, Switch.ShowConsumerInformation,
                                Switch.ShowExtendedDescriptions, Switch.ShowExtendedTechnicalSpecifications
                            }
                };

            VehicleDescription temp = _vinService.describeVehicle(mSifsir);
            if (temp != null)
            {
                if (temp.responseStatus.responseCode.Equals(ResponseStatusResponseCode.Successful) ||
                    temp.responseStatus.responseCode.Equals(ResponseStatusResponseCode.ConditionallySuccessful))
                    return true;
            }

            return false;
        }

        public List<ExtendedFactoryOptions> GetPackageOptions(VehicleDescription vehicleInfo)
        {
            var listPackageOptions = new List<ExtendedFactoryOptions>();
            var hash = new HashSet<string>();
            if (vehicleInfo.factoryOption != null && vehicleInfo.factoryOption.Any())
            {
                foreach (Option fo in vehicleInfo.factoryOption)
                    if (fo.description.Any())
                    {
                        {
                            string optionsName = fo.description.FirstOrDefault();
                            if (optionsName == null || hash.Contains(optionsName)) continue;

                            if (fo.price.msrpMax > 0 && (optionsName.Contains("PKG") || optionsName.Contains("PACKAGE")))
                            {
                                var efo = new ExtendedFactoryOptions();
                                efo.SetMsrp(fo.price.msrpMax.ToString("C"));
                                efo.SetName(CommonHelper.UpperFirstLetterOfEachWord(optionsName.Replace(",", "")));
                                efo.SetStandard(fo.standard);
                                efo.SetCategoryName(fo.header != null ? fo.header.Value : string.Empty);
                                efo.Description = (fo.description.Count() >= 2) ? fo.description[1] : fo.description[0];

                                listPackageOptions.Add(efo);
                                hash.Add(optionsName);
                            }
                        }
                    }
            }

            return listPackageOptions;
        }

        public List<ExtendedFactoryOptions> GetNonInstalledOptions(VehicleDescription vehicleInfo)
        {
            var listNonInstalledOptions = new List<ExtendedFactoryOptions>();
            var hash = new HashSet<string>();
            if (vehicleInfo.factoryOption != null && vehicleInfo.factoryOption.Any())
            {
                foreach (Option fo in vehicleInfo.factoryOption)
                    if (fo.description.Any())
                    {
                        {
                            string optionsName = fo.description.FirstOrDefault();
                            if (optionsName == null || hash.Contains(optionsName)) continue;

                            if (fo.price.msrpMax > 0 && !optionsName.Contains("PKG") && !optionsName.Contains("PACKAGE"))
                            {
                                var efo = new ExtendedFactoryOptions();
                                efo.SetMsrp(fo.price.msrpMax.ToString("C"));
                                efo.SetName(CommonHelper.UpperFirstLetterOfEachWord(optionsName.Replace(",", "")));
                                efo.SetStandard(fo.standard);
                                efo.SetCategoryName(fo.header != null ? fo.header.Value : string.Empty);
                                efo.Description = fo.description.FirstOrDefault();

                                listNonInstalledOptions.Add(efo);
                                hash.Add(optionsName);
                            }
                        }
                    }
            }

            return listNonInstalledOptions;
        }
    }
}