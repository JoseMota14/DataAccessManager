// See https://aka.ms/new-console-template for more information
using DataManager.Generic;
using DataManager.Generic.Execution;
using DataManager.Models;

Console.WriteLine("Hello, World!");

var instruction = "SELECT users.*, details.* FROM users\r\nLEFT JOIN details ON users.username = details.username;";
var xml = "<data><users><user><username>Jose</username><phone>91919191</phone></user><user><username>Mota</username><phone>782122</phone></user></users><details><detail><username>Jose</username><birth>14-03-98</birth></detail></details></data>";

var session = Session.InitializeFactories().ExecuteCreation(SessionType.Csv);

var command = new Command("Select username with details", "descr");

command.AddParameter("instruction", instruction, "string", "in");
command.AddParameter("xml", xml, "string", "in");

var result = session.ExecuteQuery(command);

Console.WriteLine("la");