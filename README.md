# Santander - Developer Coding Test

## Overview
This project implements a RESTful API using ASP.NET Core to retrieve the details of the best n stories from the Hacker News API based on their score. 

## API Endpoints

### Retrieve Best Stories
- **GET** `/api/stories/best/{n}`
- Retrieves the top n stories in descending order of score.

#### Response Format
The API returns an array of objects in the following format:
```json
[
    {
        "title": "A uBlock Origin update was rejected from the Chrome Web Store",
        "uri": "https://github.com/uBlockOrigin/uBlock-issues/issues/745",
        "postedBy": "ismaildonmez",
        "time": "2019-10-12T13:43:01+00:00",
        "score": 1716,
        "commentCount": 572
    },
    ...
]
```

### How to Run the Application


1. Clone the Repository

    - git clone https://github.com/yourusername/your-repository.git
    - cd your-repository

2. Install Dependencies Ensure you have the .NET SDK installed. You can download it from [here](https://dotnet.microsoft.com/en-us/download)
3. Run the Application
    - dotnet run
4. Access the API Open your browser or a tool like Postman and navigate to:
    - https://localhost:7193/api/Stories/best?n={number}

### Assumptions
  - The user must specify a valid integer n for retrieving stories.
  - If n is greater than the available stories, the API will return as many as it can.
