﻿using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;

namespace HashCode2018
{
    internal class Program
    {
	    private static string _header;
	    private static List<string> _content;
	    private static int _rows;
	    private static int _columns;
	    private static int _vehicles;
	    private static int _rides;
	    private static int _bonus;
	    private static int _steps;
		private static char[,] _grid;
		private static List<Ride> _listOfRides = new List<Ride>();

		private static void Main(string[] args)
		{
			var baseInputFolder = ".\\InputFiles\\";

			// Example file
			var inputFileAsString = File.ReadAllText(Path.Combine(baseInputFolder,"a_example.in"));
	        Console.WriteLine("Processing a_example file...");

	        SetupEnvironment(inputFileAsString);

			var carRideResult = CalculateTimes();

			var submissionFileContent = FormatResult(carRideResult);

			File.WriteAllText(Path.Combine(baseInputFolder, "a_example.out"), submissionFileContent);

			Console.WriteLine("Done with example.\n");
		}

	    private static string FormatResult(Dictionary<int, List<int>> carRideResult)
	    {
		    var sb = new StringBuilder();

		    foreach (var carRide in carRideResult)
		    {
			    var theList = string.Empty;

			    foreach (var ride in carRide.Value)
			    {
				    theList += ride + " ";
			    }
			    var line = $"{carRide.Key} {theList.Trim()}";

			    sb.AppendLine(line);
		    }

		    return sb.ToString();

	    }

	    private static Dictionary<int, List<int>> CalculateTimes()
	    {
		    Dictionary<int,List<int>> carRideResult = new Dictionary<int, List<int>>();

		    carRideResult.Add(1, new List<int> { 0 });
		    carRideResult.Add(2, new List<int> { 2,1 });

		    return carRideResult;
	    }

		private static void SetupEnvironment(string inputFileAsString)
	    {
			var lines = inputFileAsString.Split(new[] { "\n", "\r" }, StringSplitOptions.RemoveEmptyEntries);

		    _header = lines[0];
		    _content = lines.Skip(1).ToList();

		    _rows = int.Parse(_header.Split(' ').First());
		    _columns = int.Parse(_header.Split(' ').Skip(1).First());
			_vehicles = int.Parse(_header.Split(' ').Skip(2).First());
			_rides = int.Parse(_header.Split(' ').Skip(3).First());
			_bonus = int.Parse(_header.Split(' ').Skip(4).First());
			_steps = int.Parse(_header.Split(' ').Skip(5).First());

		    _grid = new char[_columns, _rows];

			foreach (var line in _content)
		    {
			    var numbers = line.Split(' ').ToList();

				_listOfRides.Add(new Ride
			    {
				    FromPoint = new Point(int.Parse(numbers[0]), int.Parse(numbers[1])),
				    ToPoint = new Point(int.Parse(numbers[2]), int.Parse(numbers[3])),
					EarliestStart = int.Parse(numbers[4]),
					LatestFinish = int.Parse(numbers[5])
			    });
		    }
		}
    }

	public class Ride
	{
		public Point FromPoint { get; set; }
		public Point ToPoint { get; set; }
		public int EarliestStart { get; set; }
		public int LatestFinish { get; set; }
	}
}
