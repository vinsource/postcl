using System;
using System.Data;
using System.Configuration;
using System.Web;
using DataFeedsVinControlConversion.com.chrome.platform.AutomotiveDescriptionService6;

/// <summary>
/// Summary description for AutoService
/// </summary>
/// 
namespace DataFeedsVinControlConversion
{
    public class AutoService
    {

        private AccountInfo m_Ai;

        private DataFeedsVinControlConversion.com.chrome.platform.AutomotiveDescriptionService6.AutomotiveDescriptionService6 vinService = new AutomotiveDescriptionService6();


        public AccountInfo getAccountInfo()
        {
            return this.m_Ai;
        }



        public AutoService()
        {
            m_Ai = new AccountInfo();

            this.m_Ai.accountNumber = System.Configuration.ConfigurationManager.AppSettings["accountNumber"].ToString();
            this.m_Ai.accountSecret = System.Configuration.ConfigurationManager.AppSettings["accountSecret"].ToString();

            Locale l = new Locale();
            l.country = System.Configuration.ConfigurationManager.AppSettings["country"].ToString();
            l.language = System.Configuration.ConfigurationManager.AppSettings["language"].ToString();

            this.m_Ai.locale = l;

        }

        public DataVersion[] getDataVersions()
        {
            DataVersionsRequest m_dvr = new DataVersionsRequest();

            m_dvr.accountInfo = this.getAccountInfo();

            DataVersion[] temp = vinService.getDataVersions(m_dvr);
            if (temp != null && temp.Length != 0)
                return temp;
            else
                return null;


        }

        public int[] getModelYears()
        {
            ModelYearsRequest m_Myr = new ModelYearsRequest();

            m_Myr.accountInfo = this.getAccountInfo();

            int[] temp = vinService.getModelYears(m_Myr);
            if (temp != null && temp.Length != 0)
                return temp;
            else
                return null;
        }

        public Division[] getDivisions(int m_modelYear)
        {
            DivisionsRequest m_Dr = new DivisionsRequest();

            m_Dr.accountInfo = this.getAccountInfo();
            m_Dr.modelYear = m_modelYear;

            Division[] temp = vinService.getDivisions(m_Dr);
            if (temp != null && temp.Length != 0)
                return temp;
            else
                return null;
        }

        public Subdivision[] getSubdivisions(int m_modelYear)
        {
            SubdivisionsRequest m_Sdr = new SubdivisionsRequest();

            m_Sdr.accountInfo = this.getAccountInfo();
            m_Sdr.modelYear = m_modelYear;

            Subdivision[] temp = vinService.getSubdivisions(m_Sdr);
            if (temp != null && temp.Length != 0)
                return temp;
            else
                return null;
        }

        public Model[] getModelsByDivision(int m_modelYear, int m_DivisionId)
        {
            ModelsByDivisionRequest m_Mbdr = new ModelsByDivisionRequest();

            m_Mbdr.accountInfo = this.getAccountInfo();
            m_Mbdr.modelYear = m_modelYear;

            bool flag = false;

            foreach (Division dv in getDivisions(m_modelYear))
            {
                if (dv.divisionId == m_DivisionId)
                {
                    m_Mbdr.divisionId = dv.divisionId;
                    flag = true;
                    break;
                }
            }

            if (flag == true)
            {
                Model[] temp = vinService.getModelsByDivision(m_Mbdr);
                if (temp != null && temp.Length != 0)
                    return temp;
                else
                    return null;
            }

            else
                return null;



        }

        public Model[] getModelsByDivision(int m_modelYear, string m_DivisionName)
        {
            ModelsByDivisionRequest m_Mbdr = new ModelsByDivisionRequest();

            m_Mbdr.accountInfo = this.getAccountInfo();
            m_Mbdr.modelYear = m_modelYear;

            bool flag = false;

            foreach (Division dv in getDivisions(m_modelYear))
            {
                if (dv.divisionName.Equals(m_DivisionName))
                {
                    m_Mbdr.divisionId = dv.divisionId;
                    flag = true;
                    break;
                }
            }

            if (flag == true)
            {
                Model[] temp = vinService.getModelsByDivision(m_Mbdr);
                if (temp != null && temp.Length != 0)
                    return temp;
                else
                    return null;
            }

            else
                return null;



        }


        public Model[] getModelsBySubDivision(int m_subdivisionId)
        {
            ModelsBySubdivisionRequest m_Mbsdr = new ModelsBySubdivisionRequest();

            m_Mbsdr.accountInfo = this.getAccountInfo();
            m_Mbsdr.subdivisionId = m_subdivisionId;

            Model[] temp = vinService.getModelsBySubdivision(m_Mbsdr);
            if (temp != null && temp.Length != 0)
                return temp;
            else
                return null;


        }

        public DataFeedsVinControlConversion.com.chrome.platform.AutomotiveDescriptionService6.Style[] getStyles(int m_modelId)
        {
            StylesRequest m_Sr = new StylesRequest();
            m_Sr.accountInfo = this.getAccountInfo();
            m_Sr.modelId = m_modelId;


            DataFeedsVinControlConversion.com.chrome.platform.AutomotiveDescriptionService6.Style[] temp = vinService.getStyles(m_Sr);
            if (temp != null && temp.Length != 0)
                return temp;
            else
                return null;
        }



        public StyleInformation getStyleInformationFromStyleName(int m_modelYear, string m_makeName, string m_modelName, string m_trimName, string m_manufacturerModelCode, double m_wheelBase, string[] m_manufacturerOptionCodes, string[] m_equipmentDescriptions, string m_exteriorColorName, string m_interiorColorName)
        {
            StyleInformationFromStyleNameRequest m_Sifsmr = new StyleInformationFromStyleNameRequest();




            ReturnParameters rd = new ReturnParameters();
            rd.excludeFleetOnlyStyles = true;
            rd.useSafeStandards = true;
            rd.includeAvailableEquipment = true;
            rd.includeExtendedDescriptions = true;
            rd.includeConsumerInformation = false;
            rd.includeExtendedTechnicalSpecifications = true;
            rd.includeRegionSpecificStyles = false;
            rd.enableEnrichedVehicleEquipment = true;




            m_Sifsmr.accountInfo = this.getAccountInfo();
            m_Sifsmr.modelYear = m_modelYear;
            m_Sifsmr.makeName = m_makeName;
            m_Sifsmr.modelName = m_modelName;
            m_Sifsmr.trimName = m_trimName;
            m_Sifsmr.manufacturerModelCode = m_manufacturerModelCode;
            m_Sifsmr.wheelBase = m_wheelBase;
            m_Sifsmr.manufacturerOptionCodes = m_manufacturerOptionCodes;
            m_Sifsmr.equipmentDescriptions = m_equipmentDescriptions;
            m_Sifsmr.exteriorColorName = m_exteriorColorName;
            m_Sifsmr.interiorColorName = m_interiorColorName;
            m_Sifsmr.returnParameters = rd;

            StyleInformation temp = vinService.getStyleInformationFromStyleName(m_Sifsmr);
            if (temp != null)
            {

                if (temp.responseStatus.responseCode.Equals(ResponseCode.Successful))

                    return temp;
                else
                    return null;

            }
            else
                return null;
        }

        public StyleInformation getStyleInformationFromStyleId(int[] m_StyleIds, string[] m_manufacturerOptionCodes, string[] m_equipmentDescriptions, string m_exteriorColorName, string m_interiorColorName)
        {
            StyleInformationFromStyleIdRequest m_Sifsir = new StyleInformationFromStyleIdRequest();

            ReturnParameters rd = new ReturnParameters();
            rd.excludeFleetOnlyStyles = true;
            rd.useSafeStandards = true;
            rd.includeAvailableEquipment = true;
            rd.includeExtendedDescriptions = true;
            rd.includeConsumerInformation = false;
            rd.includeExtendedTechnicalSpecifications = true;
            rd.includeRegionSpecificStyles = false;
            rd.enableEnrichedVehicleEquipment = true;



            m_Sifsir.accountInfo = this.getAccountInfo();
            m_Sifsir.styleIds = m_StyleIds;
            m_Sifsir.manufacturerOptionCodes = m_manufacturerOptionCodes;
            m_Sifsir.equipmentDescriptions = m_equipmentDescriptions;
            m_Sifsir.exteriorColorName = m_exteriorColorName;
            m_Sifsir.interiorColorName = m_interiorColorName;
            m_Sifsir.returnParameters = rd;

            StyleInformation temp = vinService.getStyleInformationFromStyleId(m_Sifsir);
            if (temp != null)
            {

                if (temp.responseStatus.responseCode.Equals(ResponseCode.Successful))

                    return temp;
                else
                    return null;

            }
            else
                return null;
        }

        public VehicleInformation getVehicleInformationFromVin(string m_Vin, string m_manufacturerModelCode, string m_trimName, double m_wheelBase, string[] m_manufacturerOptionCodes, string[] m_equipmentDescriptions, string[] m_secondaryequipmentDescriptions, string m_exteriorColorName, string m_interiorColorName, int[] m_reducingStyleIds, ReturnParameters m_returnparameters)
        {
            VehicleInformationFromVinRequest m_Vifver = new VehicleInformationFromVinRequest();
            m_Vifver.accountInfo = this.getAccountInfo();
            m_Vifver.vin = m_Vin;
            m_Vifver.manufacturerModelCode = m_manufacturerModelCode;
            m_Vifver.trimName = m_trimName;
            m_Vifver.wheelBase = m_wheelBase;
            m_Vifver.manufacturerModelCode = m_manufacturerModelCode;
            m_Vifver.equipmentDescriptions = m_equipmentDescriptions;
            m_Vifver.secondaryEquipmentDescriptions = m_secondaryequipmentDescriptions;
            m_Vifver.exteriorColorName = m_exteriorColorName;
            m_Vifver.interiorColorName = m_interiorColorName;
            m_Vifver.reducingStyleIds = m_reducingStyleIds;
            m_Vifver.returnParameters = m_returnparameters;

            VehicleInformation temp = vinService.getVehicleInformationFromVin(m_Vifver);
            if (temp != null)
            {

                if (temp.responseStatus.responseCode.Equals(ResponseCode.Successful))

                    return temp;
                else
                    return null;

            }
            else
                return null;
        }

        public VehicleInformation getVehicleInformationFromVin(string m_Vin)
        {
            var rd = new ReturnParameters();
            rd.excludeFleetOnlyStyles = false;
            rd.useSafeStandards = true;
            rd.includeAvailableEquipment = true;
            rd.includeExtendedDescriptions = true;
            rd.includeConsumerInformation = true;
            rd.includeExtendedTechnicalSpecifications = true;
            rd.includeRegionSpecificStyles = false;
            rd.enableEnrichedVehicleEquipment = true;

            var m_Vifver = new VehicleInformationFromVinRequest();
            m_Vifver.accountInfo = this.getAccountInfo();
            m_Vifver.vin = m_Vin;
            m_Vifver.returnParameters = rd;

            var temp = vinService.getVehicleInformationFromVin(m_Vifver);
            if (temp != null)
            {

                if (temp.responseStatus.responseCode.Equals(ResponseCode.Successful) || temp.responseStatus.responseCode.Equals(ResponseCode.SuccessfulUsingAlternateLocale))

                    return temp;
                else
                    return null;

            }
            else
                return null;
        }

        public bool validateVinWithVehicleDescription(string m_Vin)
        {
            ValidationRequest m_Vr = new ValidationRequest();
            m_Vr.accountInfo = this.getAccountInfo();
            m_Vr.vin = m_Vin;


            ValidationResult temp = vinService.validateVinWithVehicleDescription(m_Vr);

            return temp.isValid;

        }


    }
   


}
