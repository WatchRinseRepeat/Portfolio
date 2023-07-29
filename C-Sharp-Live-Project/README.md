# C# Live Project
A .Net Core project utilizing Entity Framework. In this project I worked on an Area of a client's website dealing with rentals from a theater.
## Sections
* Rental History
* Mistory Manager
* Rental Photos
* Rental
* Linked Rental and Rental History
* Final Takeaways

### Rental History
After creating the CRUD functionality using Entity Framework I styled the index page.

![RentalIndex](https://github.com/WatchRinseRepeat/Portfolio/assets/129567713/31e84a37-9e4d-45e1-9016-12740888ce72)

I utilized Font Awesome to display a checkmark or X icon to indicate if there were any damages to the item during the Rental. The rental name was displayed in a dynamicly sized pill and the Notes/Damages were written such a way that they would truncate if too long for the box. Lastly I placed the Edit/Details/Delete buttons into a dropdown menu which appeared when clicking on vertical elipses which in turn only appeared when the RentalHistory item was being hovered.

![EditPopUp](https://github.com/WatchRinseRepeat/Portfolio/assets/129567713/f140f7bb-72c3-4cb0-9a3b-ee12d3b74da2)

Most notably, I created a filter on the page which would sort the entries based on predetermined criteria.

![RentalSort](https://github.com/WatchRinseRepeat/Portfolio/assets/129567713/f1e0dfac-04d4-4702-885c-c50aa784b110)

The roadblocks I hit during this section of the project included forgetting to manually migrate the database model, mixing up how to reference IDs and Classes in jQuery, and feeling entirely lost on how to create the filter. However, by breaking down each issue into their smaller parts I was able to learn and overcome each roadblock. I also ran into the scenario where my previous code was incompatible with my jQuery solution and had to be refactored which taught me to think ahead to future requirements when laying down initial code.

### History Manager
The Hisotry Manager model was extension of a user/administrator model and I used it to restrict CRUD functionality to authorized users.

In this section of the project I learned about authentication through Entity Framework. Roadblocks I hit were trying to create authentication from scratch until I realized that the framework already contained what I was trying to build teaching me to always understand the enviroment I'm working in to avoid needless duplication of code.

### Rental Photos
In the Rental Photos section I created a model which contained a Bit Array to store an image uploaded by the user as well as name and description information. I styled the page with Bootstrap to show each rental as a card with the image if applicable.

![RentalsIndex](https://github.com/WatchRinseRepeat/Portfolio/assets/129567713/99b71d7f-49bc-4e5c-b8d1-e508cbfdb094)

For this page the Edit/Delete buttons would appear when hovering the image. The details could be accessed by clicking anywhere else on the card. Later in the project I returned and implemented an upvote/downvote system using ajax calls to update the database and show the information on the image overlay.

![RentalOverlay](https://github.com/WatchRinseRepeat/Portfolio/assets/129567713/818fbf60-1837-40d5-9674-71ab87f0bc2c)

Additionally I added a search functionality using jQuery which would search for terms found in the Rental Photo name or description, hiding any entry that didn't match.

No major roadblocks were hit during this particular section.

### Rentals

This section was more advanced than the previous as it was three models. One parent, and two child models which were extensions of the parent.

After achieving basic CRUD functionality I worked on the Index page. I made it so that the three types of objects would display in separate sections and display information only relevant to its own type.

![RentalBase](https://github.com/WatchRinseRepeat/Portfolio/assets/129567713/1b31a428-166e-49fd-b64e-9badb2dc9c90)
![RentalEquipment](https://github.com/WatchRinseRepeat/Portfolio/assets/129567713/bd187060-3056-4859-8551-13a4b1851ec3)
![RentalRooms](https://github.com/WatchRinseRepeat/Portfolio/assets/129567713/b8060956-0480-45b2-a90d-0b6568df0abd)

Stand in images were used as placeholders.

I then worked on the Create page so that the user could indicate which type of entry to make and only the correct options would appear. I accomplished this with jQuery.

![CreateRental](https://github.com/WatchRinseRepeat/Portfolio/assets/129567713/f8826c36-585c-445b-a0c2-f5a50573f8dd)

The Edit pages were completed by making the Edit links direct to a specific edit page based on the type of entry.

Roadblocks I hit were struggling to hit the correct database objects in the Create/Edit pages.

### Linked Rental and Rental History

Among the final features I worked on was to link the Rental and RentalHistory objects so that each RentalHistory would reference what Rental it was a history for.

The roadblock I hit was in the edit page, being able to change which Rental it was associated with. The cause I found was that I was repassing the old ID number instead of the new one.

### Final Takeaways
The amount of practical learning achieved in this two-week period was invaluable. According to the instructors I was able to complete more than the average student and they assigned me stories that were not normally handed out due to difficulty because they were confident I could handle them.

I learned just how invaluable it is to have a senior tech who is willing to guide you through roadblocks as they apepar without taking over the project and doing it for you. Additionally I sharpened my ability to reason through and search for answers.
