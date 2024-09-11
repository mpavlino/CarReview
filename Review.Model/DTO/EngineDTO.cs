using System;
using System.Linq.Expressions;

namespace Review.Model.DTO {
    public class EngineDTO {
        public int ID { get; set; }

        // Property to connect Engine to Car
        public int CarID { get; set; }

        public string Name { get; set; }
        public string Cylinders { get; set; }
        public string Displacement { get; set; }
        public string Power { get; set; } // KW, HP, BHP combined
        public string Torque { get; set; }
        public string FuelSystem { get; set; }
        public string FuelType { get; set; }
        public decimal? FuelCapacity { get; set; } // In gallons or liters
        public int? TopSpeed { get; set; } // In mph or km/h
        public decimal? Acceleration { get; set; } // 0-62 mph or 0-100 km/h
        public string DriveType { get; set; }
        public string Gearbox { get; set; }
        public string FrontBrakes { get; set; }
        public string RearBrakes { get; set; }
        public string TireSize { get; set; }
        public string Length { get; set; }
        public string Width { get; set; }
        public string Height { get; set; }
        public string FrontRearTrack { get; set; }
        public string Wheelbase { get; set; }
        public string GroundClearance { get; set; }
        public string CargoVolume { get; set; }
        public string UnladenWeight { get; set; }
        public string GrossWeightLimit { get; set; }
        public string FuelEconomyCity { get; set; }
        public string FuelEconomyHighway { get; set; }
        public string FuelEconomyCombined { get; set; }
        public string CO2Emissions { get; set; }

        // SelectorExpression for mapping Engine to EngineDTO
        public static Expression<Func<Engine, EngineDTO>> SelectorExpression { get; } = e => new EngineDTO {
            ID = e.ID,
            Name = e.Name,
            CarID = e.CarID, // Include CarID to link Engine to Car
            Cylinders = e.Cylinders,
            Displacement = e.Displacement,
            Power = e.Power,
            Torque = e.Torque,
            FuelSystem = e.FuelSystem,
            FuelType = e.FuelType,
            FuelCapacity = e.FuelCapacity,
            TopSpeed = e.TopSpeed,
            Acceleration = e.Acceleration,
            DriveType = e.DriveType,
            Gearbox = e.Gearbox,
            FrontBrakes = e.FrontBrakes,
            RearBrakes = e.RearBrakes,
            TireSize = e.TireSize,
            Length = e.Length,
            Width = e.Width,
            Height = e.Height,
            FrontRearTrack = e.FrontRearTrack,
            Wheelbase = e.Wheelbase,
            GroundClearance = e.GroundClearance,
            CargoVolume = e.CargoVolume,
            UnladenWeight = e.UnladenWeight,
            GrossWeightLimit = e.GrossWeightLimit,
            FuelEconomyCity = e.FuelEconomyCity,
            FuelEconomyHighway = e.FuelEconomyHighway,
            FuelEconomyCombined = e.FuelEconomyCombined,
            CO2Emissions = e.CO2Emissions
        };
    }
}
