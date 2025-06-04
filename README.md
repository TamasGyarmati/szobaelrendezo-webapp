# Room Furniture Arrangement App

## Project Description
This application arranges given furniture items within a room so that none of them overlap. The output is presented in a tabular format where each table cell represents 10 cm. For example, if a bed measures 160 cm by 200 cm and is located in a corner, it occupies 16 cells in width and 20 cells in length. These cells are merged, colored with a random color, and labeled with the furniture name, such as "Bed".

## Input
- Room dimensions: width and length (in cm)
- List of furniture items: each with a name, width, and length (in cm)

## Output
- A generated room layout plan showing the furniture arrangement in a grid format

## Technologies Used
- ASP.NET Core REST API
- HTML, CSS, and JavaScript for the front-end
- C# for backend logic

## Project Type
- Semester project

---

## How to Use
1. Provide the room dimensions and furniture list via the front-end interface.
2. The app computes a furniture arrangement without overlaps.
3. View the generated layout in the browser, with merged and colored cells representing each furniture piece.

---

## Example
If the room is 300 cm wide and 300 cm long, and includes a bed (160x200 cm) and a table (100x120 cm), the app will display a grid with merged cells colored and labeled accordingly, accurately representing their sizes and positions.
