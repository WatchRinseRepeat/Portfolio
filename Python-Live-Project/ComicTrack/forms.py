from django.forms import ModelForm
from django import forms
from .models import Comic


# Create the comic form based on the Comic Model
class ComicForm(ModelForm):
    class Meta:
        model = Comic
        fields = '__all__'


class SearchForm(forms.Form):
    search_Term = forms.CharField(max_length=25)
