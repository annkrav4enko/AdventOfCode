using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using Shared;

namespace Day5
{
    
    public class Part2
    {
        public void Run()
        {
            Console.WriteLine("Starting reading from file");

            var fileReader = new FileReader();

            var seedsFile = fileReader.ReadFile("inputs/seeds.txt");
            var seedToSoilFile = fileReader.ReadFile("inputs/SeedToSoil.txt");
            var soilToFertilizerFile = fileReader.ReadFile("inputs/SoilToFertilizer.txt");
            var fertilizerToWaterFile = fileReader.ReadFile("inputs/FertilizerToWater.txt");
            var waterToLightFile = fileReader.ReadFile("inputs/WaterToLight.txt");
            var lightToTemperatureFile = fileReader.ReadFile("inputs/LightToTemperature.txt");
            var temperatureToHumidityFile = fileReader.ReadFile("inputs/TemperatureToHumidity.txt");
            var humidityToLocationFile = fileReader.ReadFile("inputs/HumidityToLocation.txt");

            var seedsRange = seedsFile[0].Split(" ").Select(i => Int64.Parse(i)).ToList<Int64>();

            var seeds = new List<long>();
            for(int i = 0; i< seedsRange.Count /2 ; i++)
            {
                var firstSeed = seedsRange[2 * i];
                var range = seedsRange[2 * i + 1];

                for( int j = 0; j< range; j++)
                {
                    seeds.Add(firstSeed + j);
                }
            }


            var seedToSoil = ConvertToMap(seedToSoilFile);
            var soilToFertilizer = ConvertToMap(soilToFertilizerFile);
            var fertilizerToWater = ConvertToMap(fertilizerToWaterFile);
            var waterToLight = ConvertToMap(waterToLightFile);
            var lightToTemperature = ConvertToMap(lightToTemperatureFile);
            var temperatureToHumidity = ConvertToMap(temperatureToHumidityFile);
            var humidityToLocation = ConvertToMap(humidityToLocationFile);

            var locations = new List<long>();

            foreach (var seed in seeds)
            {
                Console.WriteLine($"Seed: {seed}");
                var soil = FindCorresponding(seedToSoil, seed);
                var fertilizer = FindCorresponding(soilToFertilizer, soil);
                var water = FindCorresponding(fertilizerToWater, fertilizer);
                var light = FindCorresponding(waterToLight, water);
                var temperature = FindCorresponding(lightToTemperature, light);
                var humidity = FindCorresponding(temperatureToHumidity, temperature);
                var location = FindCorresponding(humidityToLocation, humidity);

                locations.Add(location);
            }

            var result = locations.Min();

            Console.WriteLine(result);
        }

        private long FindCorresponding(List<Map> map, long sourceValue)
        {
            var destLine = map.Where(i => i.Source < sourceValue && sourceValue < i.Source + i.Range).FirstOrDefault();

            long correspondingDestination = destLine != null ? sourceValue + destLine.Destination - destLine.Source : sourceValue;
            Console.WriteLine($"    Corresponding Value: {correspondingDestination}");
            return correspondingDestination;
        }

        private List<Map> ConvertToMap(List<string> file)
        {
            var maps = file.Select(line => (line.Split(" ").Select(i => Int64.Parse(i))).ToList()).Select(map => new Map
            {
                Destination = map[0],
                Source = map[1],
                Range = map[2]
            }).ToList();

            foreach (var line in maps)
            {
                Console.WriteLine($"  {line.Destination} {line.Source} {line.Range}");
            }
            Console.WriteLine(maps);
            return maps;
        }
    }

}
