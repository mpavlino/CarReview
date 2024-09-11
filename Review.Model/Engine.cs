using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Review.Model {
    public class Engine {
        public int ID { get; set; }
        public string Name { get; set; }

        // Engine specifications
        public string Cylinders { get; set; }
        public string Displacement { get; set; }
        public string Power { get; set; } // Can include details like KW, HP, BHP
        public string Torque { get; set; }
        public string FuelSystem { get; set; }
        public string FuelType { get; set; }
        public decimal? FuelCapacity { get; set; } // In gallons or liters

        // Performance specs
        public int? TopSpeed { get; set; } // In mph or km/h
        public decimal? Acceleration { get; set; } // 0-62 mph or 0-100 km/h

        // Transmission specs
        public string DriveType { get; set; }
        public string Gearbox { get; set; }

        // Brakes specs
        public string FrontBrakes { get; set; }
        public string RearBrakes { get; set; }

        // Tires specs
        public string TireSize { get; set; }

        // Dimensions
        public string Length { get; set; }
        public string Width { get; set; }
        public string Height { get; set; }
        public string FrontRearTrack { get; set; }
        public string Wheelbase { get; set; }
        public string GroundClearance { get; set; }
        public string CargoVolume { get; set; }

        // Weight specs
        public string UnladenWeight { get; set; }
        public string GrossWeightLimit { get; set; }

        // Fuel economy
        public string FuelEconomyCity { get; set; }
        public string FuelEconomyHighway { get; set; }
        public string FuelEconomyCombined { get; set; }
        public string CO2Emissions { get; set; }

        // Navigation properties
        [ForeignKey( nameof( Car ) )]
        public int CarID { get; set; }
        public Car Car { get; set; }
    }
}
