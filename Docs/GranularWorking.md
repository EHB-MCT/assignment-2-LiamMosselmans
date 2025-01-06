# DatabaseManager Script

## Overview
The `DatabaseManager` script handles interactions with Firebase, including managing user data, submitting parkour results, tracking path choices, updating leaderboard data, and displaying the leaderboard UI.

### Classes & Methods

#### Class: `DatabaseManager`

Handles the interaction with Firebase and manages the submission and retrieval of parkour data.

#### Fields:

- `_databaseReference`: The reference to the Firebase Realtime Database, used for accessing and modifying data.
- `_userName`: The default user name, used when creating a new user.
- `_userID`: A unique ID based on the player's device, used to identify the player.
- `_pathChoiceCounts`: Holds the counts of how often each path (A or B) is chosen by players.
- `_topTimes`: A list of the top times in the game, sorted by fastest to slowest time.

- `leaderboardEntryPrefab`: A prefab for displaying leaderboard entries.
- `leaderboardContent`: The container for the leaderboard entries in the UI.

#### Methods:

##### `Start()`
- Initializes the user ID based on the device's unique identifier.
- Calls `InitializeFirebase()` to set up Firebase.

##### `InitializeFirebase()`
- Checks and fixes Firebase dependencies asynchronously.
- If Firebase is initialized successfully, sets up the database reference and calls `LoadLeaderboard()` to retrieve leaderboard data.
- If Firebase fails to initialize, logs an error.

##### `CreateUser()`
- Creates a new user with the default `_userName`.
- Converts the user object into JSON format and stores it in the Firebase Realtime Database under `users/{userID}`.

##### `SubmitUserParkourData(float totalTime, Dictionary<string, float> sectionTimes, string chosenPath)`
- Submits the player's parkour data (total time and chosen path) to the Firebase database.
- If no path is chosen, logs an error and prevents submission.
- Calls `TrackPathChoice()` to update path selection counts and `SubmitGlobalData()` to submit updated global data.

##### `TrackPathChoice(string chosenPath, float totalTime)`
- Increments the path choice counter (either `PathACount` or `PathBCount`) based on the chosen path.
- Calls `UpdateTopTimes()` to update the list of top times with the player's result.

##### `UpdateTopTimes(float totalTime, string chosenPath)`
- Adds the player's new time to the top times list if it is unique (doesn't already exist in the list).
- Sorts the list of top times in ascending order (fastest times first).
- Ensures that the list contains no more than 10 entries, removing the slowest time if necessary.

##### `SubmitGlobalData()`
- Creates a `GlobalData` object containing the top times and path choice counts.
- Converts the `GlobalData` object to JSON format and submits it to the Firebase Realtime Database under `globalData`.

##### `LoadLeaderboard()`
- Retrieves the global data from the Firebase Realtime Database.
- Parses the path choice counts and top times from the database snapshot.
- Calls `DisplayLeaderboard()` to update the UI with the loaded leaderboard data.

##### `DisplayLeaderboard()`
- Clears the existing leaderboard UI.
- Iterates through the top times and calls `AddLeaderboardEntry()` to create and display each leaderboard entry.

##### `AddLeaderboardEntry(TimeEntry entry)`
- Instantiates a leaderboard entry prefab and sets the text to display the player's time and chosen path.
- Adds the entry to the leaderboard UI.

---

### Classes Used in `DatabaseManager`

#### `PathChoiceCounts`
A class that holds the count of how many times each path (A or B) has been chosen by players.

#### `User`
A class representing a user, containing their name and other relevant data.

#### `ParkourData`
A class representing parkour data for a user, including their total time and chosen path.

#### `TimeEntry`
A class representing an entry in the leaderboard, containing a player's time and chosen path.

#### `GlobalData`
A class representing global game data, including the top times and path choice counts.

---

## Summary
The `DatabaseManager` script is responsible for submitting and retrieving player and global data from Firebase, updating the leaderboard, and displaying it on the UI. It tracks user progress, chosen paths, and top times, providing a competitive element to the game with Firebase integration for persistent data storage.

---

# DataTrackingManager Script

## Overview
The `DataTrackingManager` script is responsible for tracking the player's parkour path, total time, and section times during their gameplay. It interacts with the `StopWatch` component to start and stop the time tracking and submits the collected data to the `DatabaseManager`.

### Classes & Methods

#### Class: `DataTrackingManager`

Manages the tracking of the player's time and sections during their parkour run. It uses a `StopWatch` to track the total time and section times, and it submits the data to the `DatabaseManager`.

#### Fields:

- `_stopWatch`: A reference to the `StopWatch` component that handles the time tracking and section time management.
- `ChosenPath`: A string indicating the path chosen by the player (e.g., "PathA" or "PathB").
- `_totalTime`: A float representing the total time the player took to complete the parkour path.
- `_databaseManager`: A reference to the `DatabaseManager` that submits the collected data to Firebase.

#### Methods:

##### `Start()`
- Initializes the `_stopWatch` by getting the `StopWatch` component attached to the same GameObject.
- Finds the `DatabaseManager` in the scene to handle data submission.

##### `StartTrackingPath(string chosenPath)`
- Starts tracking the chosen parkour path.
- Sets the `ChosenPath` field to the given `chosenPath`.
- Calls the `StartTracking()` method on the `_stopWatch` to begin time tracking.

##### `StopTrackingPath()`
- Stops the time tracking by calling `StopTracking()` on the `_stopWatch`.
- Retrieves the total time from the `_stopWatch` and stores it in `_totalTime`.
- Submits the parkour data (total time, section times, and chosen path) to the `DatabaseManager` using `SubmitUserParkourData()`.

##### `TrackSectionTime(string sectionName)`
- Tracks the time for a specific section of the parkour path by calling `TrackSectionTime()` on the `_stopWatch` with the given section name and the chosen path.

---

## Summary
The `DataTrackingManager` script manages the player's time tracking during their parkour run, including both total time and section times. It interacts with the `StopWatch` component to collect timing data and sends the completed data (total time, section times, and chosen path) to the `DatabaseManager` for submission to the database.

---

# ParkourData Script

## Overview
The `ParkourData` class represents the data related to the player's parkour run, including the total time spent completing the run, the sections of the path (if applicable), and the chosen path.

### Classes & Methods

#### Class: `ParkourData`

Stores the data collected during a player's parkour run, such as the total time, the sections of the path (if applicable), and the chosen path. This class is used for storing and submitting the player's run data to the database.

#### Fields:

- `TotalTime`: A float that stores the total time it took for the player to complete the parkour run.
- `ChosenPath`: A string that stores the path the player chose to complete during their run (e.g., "PathA" or "PathB").
- `SectionTimes`: A list of `SectionTimeEntry` objects (currently commented out) that would represent the times for each section of the parkour path.

#### Constructors:

##### `ParkourData(float totalTime, Dictionary<string, float> sectionTimes, string chosenPath)`
- Constructor that initializes the `ParkourData` object with the total time, section times (in the form of a dictionary), and the chosen path.
- The `sectionTimes` dictionary would be converted into a list of `SectionTimeEntry` objects (though this functionality is currently commented out).

##### `ParkourData(float totalTime, string chosenPath)`
- Constructor that initializes the `ParkourData` object with just the total time and the chosen path, without the section times.

#### Methods:

##### `ConvertDictionaryToList(Dictionary<string, float> dictionary)`
- This private method (currently commented out) would convert the section times from a dictionary into a list of `SectionTimeEntry` objects. Each entry would contain the name of the section (key) and the time for that section (value).

---

## Summary
The `ParkourData` class is used to store the player's run data, including their total time, the path they chose, and potentially the times for each section of the path. This data is intended to be submitted to the database for further processing. The section time functionality is currently commented out but could be used for more detailed tracking of the player's performance on different sections of the parkour path.

---

# GlobalData Script

## Overview
The `GlobalData` class stores global data related to the parkour game, including the top times for the parkour paths and the number of times each path has been chosen. This data is used for leaderboard functionality and path usage tracking.

### Classes & Methods

#### Class: `GlobalData`

Stores the global data for the game, including the top 10 times for each parkour path and the number of times each path has been chosen by players.

#### Fields:

- `TopTimes`: A list of `TimeEntry` objects that stores the top times for the parkour paths, sorted by the fastest times.
- `PathChoiceCounts`: A `PathChoiceCounts` object that tracks how often each parkour path (Path A and Path B) has been chosen by players.

#### Constructor:

##### `GlobalData(List<TimeEntry> topTimes, PathChoiceCounts pathChoiceCounts)`
- Initializes the `GlobalData` object with a list of `TimeEntry` objects representing the top times and a `PathChoiceCounts` object representing how many times each path has been chosen.

---

#### Class: `TimeEntry`

Stores the details of a specific entry in the leaderboard, including the time and the chosen path for that entry.

#### Fields:

- `Time`: A float that stores the time taken to complete the parkour path.
- `ChosenPath`: A string that represents the chosen path for this entry, such as "PathA" or "PathB".

#### Constructor:

##### `TimeEntry(float time, string chosenPath)`
- Initializes the `TimeEntry` object with the time and chosen path.

---

#### Class: `PathChoiceCounts`

Tracks how many times each path has been chosen by players.

#### Fields:

- `PathACount`: An integer that stores the number of times "PathA" has been chosen.
- `PathBCount`: An integer that stores the number of times "PathB" has been chosen.

#### Constructor:

##### `PathChoiceCounts(int pathACount, int pathBCount)`
- Initializes the `PathChoiceCounts` object with the number of times each path has been chosen.

---

## Summary
The `GlobalData` class is used to store and manage global game data, including the top leaderboard times (`TopTimes`) and the counts of how often each path has been chosen (`PathChoiceCounts`). The `TimeEntry` class represents each leaderboard entry, and the `PathChoiceCounts` class tracks how popular each parkour path is among players.

---

# PathTrigger Script

## Overview
The `PathTrigger` class is responsible for handling the triggers that the player interacts with during the parkour path. It determines the path the player is on, tracks their progress through sections, and stops tracking when the player reaches the finish line.

### Classes & Methods

#### Class: `PathTrigger`

Handles the triggers for different sections of the parkour path and communicates with the `DataTrackingManager` to track the player's time for each section.

#### Fields:

- `_privateName`: A string that stores the name of the chosen path (either "PathA" or "PathB").
- `_dataTrackingManager`: A reference to the `DataTrackingManager` component, which is used to track the player's parkour progress.

#### Methods:

##### `Start()`
- Initializes the `_dataTrackingManager` field by finding the `DataTrackingManager` in the scene if it hasn't already been assigned in the inspector.

##### `OnTriggerEnter(Collider other)`
- This method is triggered when another collider enters the trigger zone. It checks if the colliding object is the player (by comparing the tag to `"PlayerObject"`), and based on the tag of the current trigger (`"StartTrigger_PathA"`, `"StartTrigger_PathB"`, `"SectionTrigger"`, or `"FinishTrigger"`), it performs the appropriate action:
  - **`StartTrigger_PathA`**: Sets the path to "PathA", starts tracking the path in `DataTrackingManager`, and disables the collider to prevent further triggers.
  - **`StartTrigger_PathB`**: Sets the path to "PathB", starts tracking the path in `DataTrackingManager`, and disables the collider to prevent further triggers.
  - **`SectionTrigger`**: Tracks the time for "Section1" and disables the collider to prevent further triggers.
  - **`FinishTrigger`**: Tracks the time for "Section2", stops tracking the path, and disables the collider to prevent further triggers.

#### Summary
The `PathTrigger` script is used to manage the various triggers in the parkour game. It listens for the player’s interaction with these triggers and communicates with the `DataTrackingManager` to record the player's progress, including their chosen path, section times, and total time. The script ensures that each trigger only activates once by disabling the collider after it is used.

---

# StopWatch Script

## Overview
The `StopWatch` class is responsible for tracking the time spent on a parkour path, including the total time and section times. It tracks the start time, current section time, and allows for stopping and recording times for individual sections.

### Classes & Methods

#### Class: `StopWatch`

Handles the tracking of time for both the entire parkour path and individual sections of the path.

#### Fields:

- `_startTime`: A float that stores the start time for tracking the total time.
- `_currentSectionTime`: A float that stores the start time for the current section.
- `TotalTime`: A public float that stores the total time taken for the entire path.
- `SectionTimes`: A dictionary where the key is a combination of the chosen path and section name, and the value is the time taken for that section.

#### Methods:

##### `StartTracking()`
- Starts the stopwatch by recording the current time as `_startTime` and initializing `_currentSectionTime` to the current time. This method is called when the player starts the parkour path or a section.

##### `StopTracking()`
- Stops the stopwatch by calculating the total time taken from the start to the current time and stores it in `TotalTime`. After stopping, it resets `_startTime` to 0.

##### `TrackSectionTime(string chosenPath, string sectionName)`
- Tracks the time spent on a specific section by calculating the difference between the current time and `_currentSectionTime`. It uses a combination of the chosen path and section name as a key to store the time in the `SectionTimes` dictionary. It then updates `_currentSectionTime` to the current time for the next section.

#### Summary
The `StopWatch` script is used to manage time tracking for the parkour game. It allows for starting and stopping the total time, as well as tracking the time spent on individual sections. The times for each section are stored in a dictionary with unique keys based on the path and section, enabling detailed tracking of the player's performance. The script is used by other components, such as `DataTrackingManager`, to report and save the player's times.