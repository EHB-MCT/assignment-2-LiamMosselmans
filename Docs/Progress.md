# Way of working

## 1. Decide on Unity project conventions
- Folder structure
- Scene structure
- Naming conventions

## 2. Refactor existing scripts
- Split files to adhere to SOLID-principles.
- Refactored class, method, and variable names to adhere to naming conventions.

## 3. Decide on game features
- Removed vaulting & ledge grabbing mechanics to retain a smaller project scope.
- Added power-up using factory design pattern.

## 4. Decide which data to track
- Player completion times. Reason: showcase on a leaderboard.
- Chosen path. Reason: check which parkour is too hard or too easy to improve level design.
- Section times. Reason: check if any section is problematic in terms of difficulty.

## 5. Implement data tracking using Firebase
Firebase was chosen as it is an easy-to-use & simple way of tracking data. It works well for the scope of this project.
- Install Firebase database unitypackage.
- Connect Unity project to database.
- Create endpoints.
- Filter data for top 10 times.
- Add duplication check so that there are no duplicate entries in the database for top times.

## 6. User testing to see if database works

## 7. Data visualization
- Create UI element for top 10 times with chosen path.