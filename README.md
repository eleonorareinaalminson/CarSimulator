# CarSimulator

A simple console-based car simulator written in C# that lets you control a car with a random driver fetched from an API.

## Features

- **Random Driver**: Uses RandomUser.me API to fetch a realistic driver
- **Car Controls**: Turn left/right, drive forward/backward
- **Fuel Consumption**: Car consumes fuel with each action
- **Fatigue System**: Driver gets tired and needs to rest
- **Color-coded Status**: Visual feedback for fuel and fatigue levels
- **Refueling**: Ability to refuel the car

## Requirements

- .NET 9.0 or later
- Internet connection (to fetch drivers from API)

## Installation

1. Clone the repository:
```bash
git clone [repository-url]
cd CarSimulator
```

2. Build the project:
```bash
dotnet build
```

3. Run the application:
```bash
dotnet run --project CarSimulator
```

## How to Play

1. Start the game - a random driver is fetched automatically
2. Use the menu to control the car:
   - **1**: Turn left
   - **2**: Turn right
   - **3**: Drive forward
   - **4**: Drive backward
   - **5**: Rest (resets fatigue)
   - **6**: Refuel car
   - **7**: Exit

## Game Mechanics

### Fuel
- Car starts with full tank (20 liters)
- Each action consumes 2 liters of fuel
- Car cannot move without fuel
- Color coding: Green (>50%), Yellow (20-50%), Red (<20%)

### Fatigue
- Driver gets tired after each action
- Fatigue levels: 0-4 (green), 5-8 (yellow), 9+ (red)
- Warning shown at level 7+
- Critical warning at level 10+
- Rest resets fatigue to 0

### Direction
- Car can face four directions: North, South, East, West
- Direction is displayed in Swedish in the game interface

## Project Structure

```
CarSimulator/
├── CarSimulator/                 # Main project
│   ├── Enums/
│   │   └── Direction.cs         # Direction enum
│   ├── Interfaces/              # Interfaces for dependency injection
│   │   ├── ICar.cs
│   │   ├── IDriver.cs
│   │   ├── IGameService.cs
│   │   └── IRandomUserService.cs
│   ├── Models/                  # Data models
│   │   ├── Car.cs
│   │   └── Driver.cs
│   ├── Services/                # Business logic
│   │   ├── GameService.cs
│   │   ├── MenuService.cs
│   │   └── RandomUserService.cs
│   └── Program.cs               # Entry point
├── CarSimulator.Tests/          # MSTest unit tests
│   ├── Services/
│   │   └── GameServiceTests.cs # Comprehensive service testing
│   └── MSTestSettings.cs       # Parallel execution configuration
├── CarSimulator.NUnitTests/     # NUnit tests with advanced scenarios
│   └── HungerNUnitTests.cs     # Complex state and boundary testing
└── CarSimulator.sln             # Solution file
```

## Testing Architecture & Skills Demonstration

This project showcases comprehensive testing practices and demonstrates proficiency in multiple testing frameworks and methodologies:

### Testing Frameworks
- **Dual Framework Implementation**: Both MSTest and NUnit to demonstrate versatility
- **Moq Framework**: Advanced mocking capabilities for dependency isolation
- **Coverlet**: Code coverage analysis integration

### Testing Strategies Implemented

#### 1. **Unit Testing with Dependency Injection**
- Interface-based testing using `IGameService`, `IRandomUserService`, `ICar`, `IDriver`
- Constructor injection patterns for testability
- Mock object creation and configuration

#### 2. **Comprehensive Test Coverage**
- **Boundary Testing**: HungerLevel transitions (Full→Hungry→Starving→GameOver)
- **Parametrized Testing**: Using `[TestCase]` attributes for data-driven tests
- **State Testing**: Fatigue levels, fuel consumption, direction changes
- **Integration Testing**: GameService with mocked dependencies

#### 3. **Advanced Mocking Techniques**
```csharp
// Complex mock setups with conditional returns
_hungerServiceMock.Setup(x => x.CalculateHungerLevel(It.Is<int>(h => h <= 5)))
                 .Returns(HungerLevel.Full);

// Verification of method calls
_mockRandomUserService.Verify(x => x.GetRandomDriverAsync(), Times.Once);
```

#### 4. **Test Structure & Organization**
- **AAA Pattern**: Arrange, Act, Assert consistently applied
- **Test Isolation**: Each test is independent and repeatable
- **Descriptive Naming**: Clear test method names describing scenarios
- **Setup/Teardown**: Proper test initialization with `[TestInitialize]`

#### 5. **Edge Case Testing**
- Invalid menu inputs (decimals, whitespace, out-of-range)
- Boundary conditions (fuel depletion, critical fatigue)
- API failure scenarios with fallback mechanisms

### Running Tests

#### Run MSTest Suite:
```bash
dotnet test CarSimulator.Tests --logger "console;verbosity=detailed"
```

#### Run NUnit Suite:
```bash
dotnet test CarSimulator.NUnitTests --logger "console;verbosity=detailed"
```

#### Run All Tests with Coverage:
```bash
dotnet test --collect:"XPlat Code Coverage"
```

#### Parallel Test Execution:
```bash
dotnet test --parallel
```

### Test Examples

The project includes examples of:
- **Parameterized Tests**: Multiple input scenarios in single test methods
- **Mock Verification**: Ensuring dependencies are called correctly
- **State Assertion**: Validating object state changes
- **Exception Testing**: Handling error conditions
- **Async Testing**: Testing async methods with proper Task handling

### Testing Best Practices Demonstrated
- ✅ Single Responsibility per test
- ✅ Clear test naming conventions
- ✅ Proper use of assertions
- ✅ Mock isolation and verification
- ✅ Test data organization
- ✅ Boundary and edge case coverage
- ✅ Integration between components

## API Dependency

The game uses the [RandomUser.me API](https://randomuser.me/) to fetch random drivers. If the API is unavailable, a fallback list with Swedish drivers is used.

## Technical Details

- **Language**: C# 9.0 with latest language features
- **Framework**: .NET 9.0
- **Architecture**: Clean architecture with dependency injection
- **Testing Frameworks**: 
  - MSTest 3.9.3 with parallel execution
  - NUnit 4.3.2 with advanced assertions
  - Moq 4.20.72 for sophisticated mocking
  - Coverlet for code coverage analysis
- **Design Patterns**: 
  - Dependency Injection
  - Interface Segregation
  - Single Responsibility Principle
- **HTTP Client**: HttpClient with timeout handling
- **JSON**: System.Text.Json for high-performance deserialization
- **Error Handling**: Comprehensive exception handling with fallback mechanisms

## Contributing

1. Fork the project
2. Create a feature branch (`git checkout -b feature/new-feature`)
3. Commit your changes (`git commit -am 'Add new feature'`)
4. Push to the branch (`git push origin feature/new-feature`)
5. Create a Pull Request

## Future Improvements

- [ ] Save game history
- [ ] Add position/coordinates
- [ ] More complex map/terrain
- [ ] Sound effects
- [ ] GUI version
- [ ] Multiplayer support
- [ ] Car and driver upgrades
- [ ] Weather effects
- [ ] Traffic lights and road signs

## License

This project is licensed under the MIT License - see the LICENSE file for details.
