using System;
using System.Security.Cryptography;
using System.Text;

namespace didwwapi
{
	class MainClass
	{
		// contact with support@didww.com
		const string API_USERNAME = "username@example.com"; // insert your API username here
		const string API_KEY = "SOMEAPIKEYVALUE"; // insert your API key here

		public static void Main (string[] args)
		{
			DIDWW didww = new DIDWW ();

			string auth_string = GenerateAuthString();
			string customer_id = "0";
			string country_iso = "UA";
			int period = 1;
			float prepaid_funds = 0.00f;
			string city_prefix = "";
			string city_id = "";
			int autorenew_enable = 1;
			string uniq_hash = GenerateUniqHash(DateTime.Now.Ticks.ToString());

  			MappingDataIn map_data = new MappingDataIn ();
			map_data.cli_format = "e164";
			map_data.cli_prefix = "";
			map_data.map_detail = "1234567890@example.com";
			map_data.map_itsp_id = "";
			map_data.map_pref_server = 0;
			map_data.map_proto = "SIP";
			map_data.map_type = "URI";

			service_data result = didww.didww_ordercreate (auth_string, customer_id, country_iso, city_prefix, period, map_data, prepaid_funds.ToString(), uniq_hash, city_id, autorenew_enable);

			Console.WriteLine ("DID Number is: " + result.did_number);
		}

		private static string GenerateUniqHash(string input)
		{
			MD5 md5 = MD5.Create();
			byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
			byte[] hash = md5.ComputeHash(inputBytes);

			StringBuilder sb = new StringBuilder();
			for (int i = 0; i < hash.Length; i++) {
				sb.Append(hash[i].ToString("X2"));
			}

			return sb.ToString();
		}

		private static string GenerateAuthString()
		{
			string value = string.Concat (API_USERNAME, API_KEY, "sandbox");

			var data = Encoding.ASCII.GetBytes(value);
			var hashData = new SHA1Managed().ComputeHash(data);

			var hash = string.Empty;

			foreach (var b in hashData) {
				hash += b.ToString("X2");
			}

			return hash.ToLower();

		}
	}
}
