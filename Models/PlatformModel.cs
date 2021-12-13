using System;
using System.Diagnostics;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
namespace gamed.Models 
{
	public enum catEnum 
	{
		console = 1,
		arcade,
		platform,
		operating_system,
		portable_console,
		computer
	}
	public class PlatformModel
	{
		public string abbreviation;
		public string alternative_name;
		public catEnum category;
		public Guid checksum;
		[JsonConverter(typeof(UnixDateTimeConverter))]
		public DateTime created_at;
		public int generation;
		public string name;
		public int platform_family;
		public int platform_logo;
		public string slug;
		public string summary;
		[JsonConverter(typeof(UnixDateTimeConverter))]
		public DateTime updated_at; 
		public string url;
		public int[] versions;
		public int[] websites;
	

	}
}