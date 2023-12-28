using VillaApi.Models.Dto;

namespace VillaApi.Data
{
	public class VillaStore
	{
		public static List<VillaDto> villaList = new List<VillaDto>()
		{
			new VillaDto(){Id=1, Name="Pool Villa", Sqft=100,Occupancy=4},
			new VillaDto(){Id=2, Name="Beach Villa",Sqft= 200,Occupancy=3}
		};

	}
}

// docker run -d --name sql_server -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=someThingComplicated1234' -p 1433:1433 mcr.microsoft.com/mssql/server:2019-latest
