import logging

from django.shortcuts import render, redirect, get_object_or_404
from django.http import HttpResponse
from django.core.paginator import Paginator

import requests
import json
from bs4 import BeautifulSoup

from .forms import ComicForm, SearchForm
from .models import Comic


# Create your views here.
def comictrack_home(request):
    return render(request, 'comictrack/comictrack_home.html')


def comictrack_add(request):
    form = ComicForm(data=request.POST or None) # Retrieve the comic add form
    # Check if the request method was post
    if request.method == "POST":
        if form.is_valid():  # CHeck to see if the submitted form is valid and if so, saves the form
            form.save()
            return redirect('comictrack_home')
    content = {'form': form}
    # adds content of form to page
    return render(request, 'comictrack/comictrack_add.html', content)


def comictrack_collection(request):
    comics = Comic.Comics.all()  # Grab the objects in the Comic model
    # Add pagination
    paginator = Paginator(comics, 5)  # Show 5 items per page

    page_number = request.GET.get("page")
    page_obj = paginator.get_page(page_number)
    # content = {'comics': comics}
    return render(request, 'comictrack/comictrack_collection.html', {"page_obj": page_obj})


def comictrack_details(request, pk):
    comic = get_object_or_404(Comic, pk=pk)
    context = {'comic': comic}
    return render(request, 'comictrack/comictrack_details.html', context)


def comictrack_edit(request, pk):
    comic = get_object_or_404(Comic, pk=pk)
    form = ComicForm(data=request.POST or None, instance=comic)
    if request.method == "POST":
        if form.is_valid():
            form.save()
        return redirect('../../../collection')
    content = {'form': form, 'comic': comic}
    return render(request, 'comictrack/comictrack_edit.html', content)

def comictrack_delete(request, pk):
    comic = get_object_or_404(Comic, pk=pk)
    if request.method == 'POST':
        comic.delete()
        return redirect('../../../')
    content = {'comic': comic}
    return render(request, 'comictrack/comictrack_delete.html', content)

def comictrack_soup(request):

    url = 'https://www.marvel.com/comics/calendar'   # set the url
    req = requests.get(url)  # request the url content
    scrape = req.text  # get the content in a variable
    found_comics = []  # array for the found comics

    soup = BeautifulSoup(scrape, "html.parser")  # Parse the content
    # Fill with what I want to grab
    table = soup.find('div', attrs={'class': 'JCMultiRow JCMultiRow-comic_issue'})  # grab just the comics

    # Iterate through the comics
    for row in table.findAll('div', attrs={'class': 'row-item comic-item'}):
        comic_url = row.a['href']  # get the url for the comic info page
        comic_img = row.img['src']  # get the comic cover image
        comic_alt = row.img['alt']  # get the comic title
        # dictionary for individual comic found
        found_comic = {
            'comic_url': comic_url,
            'comic_img': comic_img,
            'comic_alt': comic_alt
        }
        # Put the found comic into the comics dictionary
        found_comics.append(found_comic)

    return render(request, 'comictrack/comictrack_soup.html', {'comics': found_comics})


def comictrack_api(request):
    form = SearchForm(data=request.POST or None)  # Retrieve the comic add form
    content = {'form': form}
    if request.method == 'POST':
        if form.is_valid():
            # set the api url
            apiurl = "https://api.artic.edu/api/v1/artworks/search"
            # get the subject to search for
            target = form.cleaned_data['search_Term']  # clean the input
            parameters = {
                "fields": "image_id",
                "q": target
            }  # grab the image_id field which will allow us to grab the image in another call.
            response = requests.get(apiurl, params=parameters)  # make the api call

            image_ids = []  # array for the image_ids
            for data in response.json()['data']:  # put the image_ids into the array from the json response.
                image_ids.append(data['image_id'])
            # print(image_ids)
            return render(request, 'comictrack/comictrack_apiresults.html', {'image_ids': image_ids})

    return render(request, 'comictrack/comictrack_api.html', content)


def comictrack_apiresults(request):
    return render(request, 'comictrack/comictrack_apiresults.html')


def comictrack_soupsave(request, details):
    # parse details
    # add database object
    split_details = details.split('#')  # [0] is series title and [1] is issue number
    if len(split_details) == 2:  # help prevent bad data in the database
        # create instance of object in the database
        new_comic = Comic(publisher="Marvel", series_title=split_details[0], issue_num=split_details[1])
        new_comic.save()

        # Load the collection page info
        comics = Comic.Comics.all()  # Grab the objects in the Comic model
        # Add pagination
        paginator = Paginator(comics, 5)  # Show 5 items per page

        page_number = paginator.num_pages  # Go to last page
        page_obj = paginator.get_page(page_number)
        return render(request, 'comictrack/comictrack_success.html', {"page_obj": page_obj})
    return render(request, 'comictrack/comictrack_failed.html')
