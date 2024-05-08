using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using SalesDemo.Entities;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace SalesDemo.Web.Controllers
{
    public class TestController : Controller
    {


        public async Task<IActionResult> Index()
        {

            using (var client = new HttpClient())
            {
                var responseMessage = await client.GetAsync("https://localhost:44363/api/Company");
                var jsonString = await responseMessage.Content.ReadAsStringAsync();
                var companyDtoDatas = JsonConvert.DeserializeObject<List<CompanyDto>>(jsonString);

                //List<Company> company = new List<Company>();
                //foreach (var companyDto in companyDtoDatas)
                //{
                //    var company1 = new Company
                //    {
                //        Id = new ObjectId(companyDto.Id.TimeStamp,companyDto.Id.Machine, (short)companyDto.Id.Increment, companyDto.Id.Pid),
                //        CompanyName = companyDto.CompanyName,   
                //        PhoneNumber = companyDto.PhoneNumber,
                //        Products = companyDto.Products

                //    };
                //}


            }
            return View();
        }



        //bu fonksiyon bir List<BsonDocument> alır ve json parse ederek bsonId nin generate işlemini yapar
        //private static ObjectId calculateObjectId(BsonDocument bsonDocument, string idName)
        //{

        //    var a = bsonDocument.ToJson().ToString();
        //    JObject jsonObject = JObject.Parse(a);
        //    //ObjectId ObjectId = new ObjectId(,);

        //    var CompanyTimestamp = jsonObject[idName]["timestamp"].ToString();
        //    var companyMachine = jsonObject[idName]["machine"].ToString();
        //    var CompanyPid = jsonObject[idName]["pid"].ToString();
        //    var CompanyIncrement = jsonObject[idName]["increment"].ToString();
        //    var id = new ObjectId(timestamp: Int32.Parse(companyMachine), Int32.Parse(companyMachine), short.Parse(CompanyPid), Int32.Parse(CompanyIncrement)).ToString();
        //    var objId = ObjectId.Parse(id);
        //    return objId;

        //}
    }
}


// using (var client = new HttpClient())
//{





//    var responseMessage = await client.GetAsync("https://localhost:44363/api/Sale");
//    var jsonString = await responseMessage.Content.ReadAsStringAsync();
//    //var a = jsonString.ToBsonDocument();
//    var values = BsonSerializer.Deserialize<List<BsonDocument>>(jsonString);
//    //var values2 = JsonDocument.Parse(jsonString).ToJson<List<Sale>>;
//    List<Sale> sales = new List<Sale>();
//    foreach (var bsonDocument in values)
//    {
//        //var objId = calculateObjectId(item, "companyId");

//        var a = bsonDocument.ToJson().ToString();
//        JObject jsonObject = JObject.Parse(a);




//        //Sale sale = new Sale
//        //{
//        //    CompanyId = calculateObjectId(bsonDocument, "companyId"),
//        //    Id = calculateObjectId(bsonDocument, "id"),
//        //    SaleDate = ((DateTime)jsonObject["saleDate"]),
//        //    TotalPrice = ((double)jsonObject["totalPrice"]),
//        //    SaleDetails = jsonObject["saleDetails"].Values().ToList();
//        //};

//    }
//    return View();
