# C# Live Project
A .Net Core project utilizing Entity Framework. In this project I worked on an Area of a client's website dealing with rentals from a theater.
## Sections
* Rental History
* Mistory Manager
* Rental Photos
* Rental
* Linked Rental and Rental History

### Rental History
After creating the CRUD functionality using Entity Framework I styled the index page.

![RentalIndex](https://github.com/WatchRinseRepeat/Portfolio/assets/129567713/31e84a37-9e4d-45e1-9016-12740888ce72)

I utilized Font Awesome to display a checkmark or X icon to indicate if there were any damages to the item during the Rental. The rental name was displayed in a dynamicly sized pill and the Notes/Damages were written such a way that they would truncate if too long for the box. Lastly I placed the Edit/Details/Delete buttons into a dropdown menu which appeared when clicking on vertical elipses which in turn only appeared when the RentalHistory item was being hovered.

![EditPopUp](https://github.com/WatchRinseRepeat/Portfolio/assets/129567713/f140f7bb-72c3-4cb0-9a3b-ee12d3b74da2)

Most notably, I created a filter on the page which would sort the entries based on predetermined criteria.

![RentalSort](https://github.com/WatchRinseRepeat/Portfolio/assets/129567713/f1e0dfac-04d4-4702-885c-c50aa784b110)

The roadblocks I hit during this section of the project included forgetting to manually migrate the database model, mixing up how to reference IDs and Classes in jQuery, and feeling entirely lost on how to create the filter. However, by breaking down each issue into their smaller parts I was able to learn and overcome each roadblock.

### History Manager
