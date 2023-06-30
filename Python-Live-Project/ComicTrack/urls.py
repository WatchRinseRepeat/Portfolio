from django.urls import path
from . import views


urlpatterns = [
    # Set path to the homepage for the app
    path('', views.comictrack_home, name='comictrack_home'),
    path('add/', views.comictrack_add, name='comictrack_add'),
    path('collection/', views.comictrack_collection, name='comictrack_collection'),
    path('comic/<int:pk>/details/', views.comictrack_details, name='comictrack_details'),
    path('comic/<int:pk>/edit/', views.comictrack_edit, name='comictrack_edit'),
    path('comic/<int:pk>/delete/', views.comictrack_delete, name='comictrack_delete'),
    path('soup/', views.comictrack_soup, name='comictrack_soup'),
    path('api/', views.comictrack_api, name='comictrack_api'),
    path('api/results/', views.comictrack_apiresults, name='comictrack_apiresults'),
    path('soup/<str:details>/add', views.comictrack_soupsave, name='comictrack_soupsave'),
]
