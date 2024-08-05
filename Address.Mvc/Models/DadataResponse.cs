namespace Address.Mvc.Models
{
    public class DadataResponse
    {
        public string Source { get; set; }
        public string Result { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }

        public class Metro
        {
            public string Name { get; set; }
            public string Line { get; set; }
            public double Distance { get; set; }
        }

        public class DadataAddressResponse
        {
            public string Source { get; set; }
            public string Result { get; set; }
            public string PostalCode { get; set; }
            public string Country { get; set; }
            public string CountryIsoCode { get; set; }
            public string FederalDistrict { get; set; }
            public string RegionFiasId { get; set; }
            public string RegionKladrId { get; set; }
            public string RegionIsoCode { get; set; }
            public string RegionWithType { get; set; }
            public string RegionType { get; set; }
            public string RegionTypeFull { get; set; }
            public string Region { get; set; }
            public string CityArea { get; set; }
            public string CityDistrictWithType { get; set; }
            public string CityDistrictType { get; set; }
            public string CityDistrictTypeFull { get; set; }
            public string CityDistrict { get; set; }
            public string StreetFiasId { get; set; }
            public string StreetKladrId { get; set; }
            public string StreetWithType { get; set; }
            public string StreetType { get; set; }
            public string StreetTypeFull { get; set; }
            public string Street { get; set; }
            public string HouseFiasId { get; set; }
            public string HouseKladrId { get; set; }
            public string HouseCadnum { get; set; }
            public string HouseType { get; set; }
            public string HouseTypeFull { get; set; }
            public string House { get; set; }
            public string FlatFiasId { get; set; }
            public string FlatCadnum { get; set; }
            public string FlatType { get; set; }
            public string FlatTypeFull { get; set; }
            public string Flat { get; set; }
            public double? FlatArea { get; set; }
            public double? SquareMeterPrice { get; set; }
            public double? FlatPrice { get; set; }
            public string FiasId { get; set; }
            public string FiasCode { get; set; }
            public int FiasLevel { get; set; }
            public int FiasActualityState { get; set; }
            public string KladrId { get; set; }
            public string Okato { get; set; }
            public string Oktmo { get; set; }
            public string TaxOffice { get; set; }
            public string TaxOfficeLegal { get; set; }
            public string Timezone { get; set; }
            public double? GeoLat { get; set; }
            public double? GeoLon { get; set; }
            public string BeltwayHit { get; set; }
            public double? BeltwayDistance { get; set; }
            public int QcGeo { get; set; }
            public int QcComplete { get; set; }
            public int QcHouse { get; set; }
            public int Qc { get; set; }
            public string UnparsedParts { get; set; }
            public List<Metro> Metro { get; set; }
        }
    }
}
