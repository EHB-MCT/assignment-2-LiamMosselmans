# Data Aggregation & Visualization For Parkour Game

This project is developed as part of the Development V course within the Multimedia & Creative Technology program. The focus of this project is to implement a system for aggregating and visualizing player data to enhance level design and provide players with meaningful insights into their performance.

Key metrics such as completion times, individual section times, and chosen path are tracked. Additionally, visualized statistics in the form of a leaderboard is presented to players, fostering engagement by allowing them to track their progress and compare performance.

## Key Features

- **Data Aggregation System** for tracking player performance, including completion times and chosen path.
- **Player Stats Visualization** presenting statistics to players, enhancing engagement and allowing them to track their performance.
- **Modular Code Structure** designed for easy expansion and adaptability, supporting future gameplay mechanics and features.
- **Level Design & Interaction** focused on creating paths with multiple options for traversal, allowing data tracking to optimize design choices.

# Table of Contents

- [How to Run](#how-to-run)
- [Data flow](#data-flow)
- [Controls](#player-controls)
- [License](#license)
- [Code of Conduct](#code-of-conduct)
- [Contributing Guidelines](#contributing-guidelines)
- [Sources](#sources)

## How to Run

1. Clone the Git repository.
2. Install and open Unity Hub.
3. Go to the "Projects" tab.
4. Click "Add".
5. Click "Add project from disc" and add the cloned repository as a project.
6. Open the project using the "2022.3.50f1" editor version.
7. Open the "Project" hierarchy window in the editor.
8. Go to "Assets" --> open "Scenes" folder --> open "ParkourLevel.unity".
9. Press the play button at the top of the program to run the game.

## Data Flow

1. **Data Collection:**
   - While the player is making their way through either path A or path B, data on their total time, section times, and chosen path is collected.

2. **Data Submission:**
   - Once the player completes a parkour path, a unique ID is created based on their device. This ID, along with their chosen path and total time, is sent to the database for storage.

3. **Leaderboard Update:**
   - After the data is submitted, the player's total time is compared to the top 10 times stored in the database. If the player's time qualifies, it will be added to the leaderboard.
   - Each path also has a counter to track how often it has been picked by players. These counters are updated in the database each time a player completes a path.

4. **Leaderboard Display:**
   - When the player re-enters the game, the top 10 times and the corresponding paths are retrieved from the database. This information is then displayed on the leaderboard UI for the player to view.

## Player Controls

### Controls
- **Tab**: open or close the leaderboard UI.
- **WASD**: omni-directional movement.
- **Shift**: move at running speed.
- **Spacebar**: jumping mechanic while on the ground or on a wall.
### Other Mechanics
- **Wallrunning**: the ability to run on colored walls for a small amount of time.
- **Wall Jump**: the ability to jump off a wall while wallrunning.
- **Speedboost Power-up**: a green power-up which increases the player's speed for a short while.

## License

[MIT](./LICENSE.md)

## Code of Conduct

Please adhere to the rules found in the following document: [Code of Conduct](./CODE_OF_CONDUCT.md).

## Contributing Guidelines

When contributing, please adhere to the guidelines found in the following document: [Contributing Guidelines](./CONTRIBUTING.md).

## Sources 

- [Power-up Functionality](https://www.youtube.com/watch?v=CLSiRf_OrBk) is used in the PowerUpBase.cs class & IPowerUp.cs interface
- [Firebase Realtime Database Setup](https://www.youtube.com/watch?v=59RBOBbeJaA) is used in the DatabaseManager.cs class
- [Factory Design Pattern](https://unity.com/resources/level-up-your-code-with-game-programming-patterns) is used in the PowerUpFactory.cs and SpeedBoostPowerUpFactory.cs classes
- [Factory Design Pattern](https://www.youtube.com/watch?v=lJMY0YdaY9c)
- [Factory Design Pattern](https://unity.com/how-to/how-use-factory-pattern-object-creation-runtime)
- [Factory Design Pattern](https://medium.com/@Code_With_K/enhancing-unity-game-development-with-the-factory-method-design-pattern-74163614a6ac)
- [SOLID-principles](https://www.baeldung.com/solid-principles)
- [SOLID-principles](https://www.youtube.com/watch?v=QDldZWvNK_E)
- [SOLID-principles](https://www.youtube.com/watch?v=kF7rQmSRlq0)
- [SOLID-principles](https://www.youtube.com/watch?v=eS3ny8mPn2c&list=PLcRSafycjWFfaHAnpFudWYRl7dK9w2nud)
### Research Sources
- [Research Sources](./Docs/Research.md)