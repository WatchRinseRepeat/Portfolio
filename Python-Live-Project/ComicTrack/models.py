from django.db import models

# Create your models here.


# choices for months
MONTHS = [
    ("Jan", "January"),
    ("Feb", "February"),
    ("Mar", "March"),
    ("Apr", "April"),
    ("May", "May"),
    ("Jun", "June"),
    ("Jul", "July"),
    ("Aug", "August"),
    ("Sep", "September"),
    ("Oct", "October"),
    ("Nov", "November"),
    ("Dec", "December")
]


class Comic(models.Model):
    publisher = models.CharField(max_length=30)
    series_title = models.CharField(max_length=50)
    issue_num = models.IntegerField()
    release_year = models.IntegerField(blank=True, null=True)
    release_month = models.CharField(max_length=3, choices=MONTHS, blank=True, null=True)
    owned = models.BooleanField(blank=True, null=True)
    read = models.BooleanField(blank=True, null=True)

    # Define the model Manager for Comics
    Comics = models.Manager()

    # Allow References to a specific comic to be returned as Title-Issue
    def __str__(self):
        return self.series_title + ' ' + self.issue_num
