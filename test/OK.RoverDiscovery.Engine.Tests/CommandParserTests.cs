using OK.RoverDiscovery.Core.Enums;
using OK.RoverDiscovery.Core.Interfaces;
using OK.RoverDiscovery.Core.Models;
using OK.RoverDiscovery.Engine.Exceptions;
using OK.RoverDiscovery.Engine.Implementations;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace OK.RoverDiscovery.Engine.Tests
{
    public class CommandParserTests
    {
        private readonly IPlateauFactory _plateauFactory;

        public CommandParserTests()
        {
            _plateauFactory = new PlateauFactory();
        }

        private ICommandParser GetCommandParser() => new CommandParser(_plateauFactory);

        [Fact]
        public void Parse_ShouldThrowException_WhenInputLinesAreNotValid()
        {
            string commandString = new StringBuilder()
                .AppendLine("5 5")
                .ToString();

            ICommandParser commandParser = GetCommandParser();

            Assert.Throws<CommandParserException>(() => commandParser.Parse(commandString));
        }

        [Fact]
        public void Parse_ShouldThrowException_WhenInputRoverLinesAreNotValid()
        {
            string commandString = new StringBuilder()
                .AppendLine("5 5")
                .AppendLine("0 5 E")
                .AppendLine("LMLM")
                .AppendLine("3 4 C")
                .ToString();

            ICommandParser commandParser = GetCommandParser();

            Assert.Throws<CommandParserException>(() => commandParser.Parse(commandString));
        }

        [Fact]
        public void Parse_ShouldThrowException_WhenPlateauCoordinateIsNotValid()
        {
            string commandString = new StringBuilder()
                .AppendLine("5")
                .AppendLine("0 5 E")
                .AppendLine("LMLM")
                .ToString();

            ICommandParser commandParser = GetCommandParser();

            Assert.Throws<CommandParserException>(() => commandParser.Parse(commandString));
        }

        [Fact]
        public void Parse_ShouldThrowException_WhenPlateauCoordinateXIsNotNumeric()
        {
            string commandString = new StringBuilder()
                .AppendLine("A 5")
                .AppendLine("0 5 E")
                .AppendLine("LMLM")
                .ToString();

            ICommandParser commandParser = GetCommandParser();

            Assert.Throws<CommandParserException>(() => commandParser.Parse(commandString));
        }

        [Fact]
        public void Parse_ShouldThrowException_WhenPlateauCoordinateYIsNotNumeric()
        {
            string commandString = new StringBuilder()
                .AppendLine("5 C")
                .AppendLine("0 5 E")
                .AppendLine("LMLM")
                .ToString();

            ICommandParser commandParser = GetCommandParser();

            Assert.Throws<CommandParserException>(() => commandParser.Parse(commandString));
        }

        [Fact]
        public void Parse_ShouldThrowException_WhenRoverPositionIsNotValid()
        {
            string commandString = new StringBuilder()
                .AppendLine("5 5")
                .AppendLine("0 5")
                .AppendLine("LMLM")
                .ToString();

            ICommandParser commandParser = GetCommandParser();

            Assert.Throws<CommandParserException>(() => commandParser.Parse(commandString));
        }

        [Fact]
        public void Parse_ShouldThrowException_WhenRoverPositionCoordinateXIsNotNumeric()
        {
            string commandString = new StringBuilder()
                .AppendLine("5 5")
                .AppendLine("A 5 E")
                .AppendLine("LMLM")
                .ToString();

            ICommandParser commandParser = GetCommandParser();

            Assert.Throws<CommandParserException>(() => commandParser.Parse(commandString));
        }

        [Fact]
        public void Parse_ShouldThrowException_WhenRoverPositionCoordinateYIsNotNumeric()
        {
            string commandString = new StringBuilder()
                .AppendLine("5 5")
                .AppendLine("0 D E")
                .AppendLine("LMLM")
                .ToString();

            ICommandParser commandParser = GetCommandParser();

            Assert.Throws<CommandParserException>(() => commandParser.Parse(commandString));
        }

        [Fact]
        public void Parse_ShouldThrowException_WhenRoverPositionDirectionIsNotValid()
        {
            string commandString = new StringBuilder()
                .AppendLine("5 5")
                .AppendLine("0 5 L")
                .AppendLine("LMLM")
                .ToString();

            ICommandParser commandParser = GetCommandParser();

            Assert.Throws<CommandParserException>(() => commandParser.Parse(commandString));
        }

        [Fact]
        public void Parse_ShouldThrowException_WhenRoverCommandIsNotValid()
        {
            string commandString = new StringBuilder()
                .AppendLine("5 5")
                .AppendLine("0 5 E")
                .AppendLine("LMLMC")
                .ToString();

            ICommandParser commandParser = GetCommandParser();

            Assert.Throws<CommandParserException>(() => commandParser.Parse(commandString));
        }

        [Fact]
        public void Parse_ShouldWorkCorrectly_WhenInputIsValid()
        {
            string commandString = new StringBuilder()
                .AppendLine("5 5")
                .AppendLine("0 5 E")
                .AppendLine("LMLM")
                .ToString();

            ICommandParser commandParser = GetCommandParser();

            commandParser.Parse(commandString);

            Assert.NotNull(commandParser.Plateau);
            Assert.Equal(6, commandParser.Plateau.Width);
            Assert.Equal(6, commandParser.Plateau.Height);
            Assert.Equal(0, commandParser.Plateau.Origin.X);
            Assert.Equal(0, commandParser.Plateau.Origin.Y);

            Assert.NotNull(commandParser.Rovers);
            Assert.Single(commandParser.Rovers);

            PositionModel position = commandParser.Rovers[0].Position;

            Assert.NotNull(position);
            Assert.Equal(DirectionEnum.E, position.Direction);
            Assert.NotNull(position.Coordinate);
            Assert.Equal(0, position.Coordinate.X);
            Assert.Equal(5, position.Coordinate.Y);

            List<CommandEnum> commands = commandParser.Rovers[0].Commands;

            Assert.NotNull(commands);
            Assert.Equal(4, commands.Count);
        }
    }
}