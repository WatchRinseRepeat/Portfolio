# ComicTrack
## Python Live Project

The Python live project was a two week team project in which we worked on pieces of a django site. I worked on an App within the site called **ComicTrack** which allowed for the tracking of comic book issues in a collection. I kept the styling simple as this particular project was focuing on the backend principles but I did some basic styling to suggest the theme of the app such as fonts and background.

### Aspects
* CRUD Functionality
* HTML Scraping using Beautiful Soup
* API management

#### Crud Functionality
I began the project by creating a database model with CRUD functionality.

![Add](https://github.com/WatchRinseRepeat/Portfolio/assets/129567713/03487458-fe1c-46b8-bc0e-c0c6b0053919)
![CRUD](https://github.com/WatchRinseRepeat/Portfolio/assets/129567713/8e9fb242-6883-4a85-853e-e8a737919ad3)

I kept the data to be tracked simple. Just the key details that would be important when tracking one's comic book collection. Once I had the CRUD pages completed I moved on to Index page from which the collection as a whole could be viewed. I decided to add pagination to the project as an extra feature.

![Collection](https://github.com/WatchRinseRepeat/Portfolio/assets/129567713/38e49e3b-e5ff-4f21-a09f-eda859e7855a)

#### HTML Scraping
Once I accomplished the required CRUD functionlity of the app I moved onto the optional features presented for the project. First was HTML scraping using Beautiful Soup. To keep with the theme of the app I selected [Marvel's Upcoming Comics Calendar](https://www.marvel.com/comics/calendar). I was able to narrow down the elements to grab the image of the comic cover as well as the comic title, issue number, and link to Marvel's site featuring that comic. Additionally I added a link which allowed for any scraped item to be added to the database.
![Soup](https://github.com/WatchRinseRepeat/Portfolio/assets/129567713/8a382abc-f3f9-4ab2-822a-f0f53f035be3)

#### API Management
The last section of optional features was to create a page that utilized API calls to display content. I had hoped to utilize Marvel's API however it's requirements did not meet the environment I was developing in so I pivoted to use the Art Insitute of Chicago's API to create a page which could take search terms and display works from the Institute's archives.
![api2](https://github.com/WatchRinseRepeat/Portfolio/assets/129567713/1a3ec7e0-6ab2-43f6-a086-b5a9523db274)

### Take Aways
