using OK.RoverDiscovery.Core.Enums;
using OK.RoverDiscovery.Core.Interfaces;
using OK.RoverDiscovery.Core.Models;
using OK.RoverDiscovery.Engine.Exceptions;
using OK.RoverDiscovery.Engine.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OK.RoverDiscovery.Engine.Implementations
{
    public class CommandParser : ICommandParser
    {
        public PlateauModel Plateau { get; private set; }

        public List<RoverModel> Rovers { get; private set; }

        private const char CommandLineSeparator = '\n';
        private const char PositionSeparator = ' ';

        private readonly IPlateauFactory _plateauFactory;

        public CommandParser(IPlateauFactory plateauFactory)
        {
            _plateauFactory = plateauFactory ?? throw new ArgumentNullException(nameof(plateauFactory));
        }

        public void Parse(string commandString)
        {
            List<string> lines = commandString.Split(CommandLineSeparator)
                                              .Select(x => x.Trim())
                                              .Where(x => !string.IsNullOrEmpty(x))
                                              .ToList();

            if (lines.Count < 3 || lines.Count % 2 == 0)
            {
                throw new CommandParserException($"Invalid Line Count");
            }

            ParsePlateau(lines[0]);

            ParseRovers(lines.Skip(1).ToList());
        }

        private void ParsePlateau(string plateauLine)
        {
            List<string> plateauCoordinates = plateauLine.Split(PositionSeparator)
                                                         .Select(x => x.Trim())
                                                         .ToList();

            if (plateauCoordinates.Count != 2)
            {
                throw new CommandParserException($"Invalid Plateau Coordinate: {plateauLine}");
            }

            if (!int.TryParse(plateauCoordinates[0], out int plateauCoordinateX))
            {
                throw new CommandParserException($"Invalid Plateau Coordinate: {plateauCoordinates[0]}");
            }

            if (!int.TryParse(plateauCoordinates[1], out int plateauCoordinateY))
            {
                throw new CommandParserException($"Invalid Plateau Coordinate: {plateauCoordinates[1]}");
            }

            Plateau = _plateauFactory.Create(new CoordinateModel(plateauCoordinateX, plateauCoordinateY));
        }

        private void ParseRovers(List<string> roverLines)
        {
            Rovers = new List<RoverModel>();

            RoverModel rover = new RoverModel();

            for (int i = 0; i < roverLines.Count; i++)
            {
                if (i % 2 == 0)
                {
                    List<string> positions = roverLines[i].Split(PositionSeparator)
                                                          .Select(x => x.Trim())
                                                          .ToList();

                    if (positions.Count != 3)
                    {
                        throw new CommandParserException($"Invalid Rover Position: {roverLines[i]}");
                    }

                    if (!int.TryParse(positions[0], out int roverCoordinateX))
                    {
                        throw new CommandParserException($"Invalid Rover Position: {positions[0]}");
                    }

                    if (!int.TryParse(positions[1], out int roverCoordinateY))
                    {
                        throw new CommandParserException($"Invalid Rover Position: {positions[1]}");
                    }

                    rover.Position = new PositionModel()
                    {
                        Coordinate = new CoordinateModel(roverCoordinateX, roverCoordinateY),
                        Direction = GetDirectionFromString(positions[2])
                    };

                    if (rover.Position.IsOutOfPlateauBound(Plateau))
                    {
                        throw new CommandParserException($"Out Of Bound: ({rover.Position.Coordinate.X}, {rover.Position.Coordinate.Y})");
                    }
                }
                else
                {
                    rover.Commands = roverLines[i].Replace(" ", string.Empty)
                                                  .ToArray()
                                                  .Select(x => GetCommandFromString(x.ToString()))
                                                  .ToList();

                    Rovers.Add(rover);

                    rover = new RoverModel();
                }
            }
        }

        private DirectionEnum GetDirectionFromString(string str)
        {
            if (!Enum.TryParse(str, out DirectionEnum direction))
            {
                throw new CommandParserException($"Invalid Direction: {str}");
            }

            return direction;
        }

        private CommandEnum GetCommandFromString(string str)
        {
            if (!Enum.TryParse(str, out CommandEnum command))
            {
                throw new CommandParserException($"Invalid Command: {str}");
            }

            return command;
        }
    }
}