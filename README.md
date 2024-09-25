<h1 align="left"> Customer feedback </h1>

<!-- START doctoc generated TOC please keep comment here to allow auto update -->
<!-- DON'T EDIT THIS SECTION, INSTEAD RE-RUN doctoc TO UPDATE -->
## Table of Contents

- [Table of Contents](#table-of-contents)
- [Overview](#overview)
- [Technologies](#technologies)
- [Project structure](#project-structure)
- [How to run](#how-to-run)
  - [Step one](#step-one)
  - [Step two](#step-two)
  - [Step three](#step-three)

<!-- END doctoc generated TOC please keep comment here to allow auto update -->

## Overview

A customer feedback application receives and processes feedback messages at a high rate. The client application sends feedback messages to a REST API endpoint in bulk (an array of feedback messages) at a rate of 2,000 messages per second. 

## Technologies

- **Vertical Slice Architecture** for architecture level
- **Postgres** for write side of **feedback** microservice
- **MongoDB** for read side of **analytics** microservices
- **Rabbitmq** on top of **Masstransit** for **Event driven architecture** between our microservices
- **Minimal API** for endpoints
- **Swagger tools** for documenting API's
- **Entityframework core** as main ORM
- **Serilog** for structured logging
- **Seq** for structured logging centralization
- **Fluent validation** for building strongly-typed validation rules
- **Flunt results** for Result pattern
- **Asp versioning** for API's versioning

## Project structure

![alt text](https://i.imgur.com/4BHefsZ.png)

In this project I've used [vertical slice architecture](https://jimmybogard.com/vertical-slice-architecture/) + [feature folder structure](http://www.kamilgrzybek.com/design/feature-folders/).
I treat each request as a distinct use case or slice, encapsulating and grouping all concerns together.
This approach minimize coupling between slices and maximize coupling in a slice.
Each slice can decide for itself how to best fulfill the request.
When new feature is added, there is only add code.
Side effects are almost none, and shared code is really changed.

I used RabbitMQ as message broker for async communication between microservices using the eventual consistency mechanism.
Each microservice uses MassTransit for doing broker communications.
Microservices are event based which means they can publish and/or subscribe to any events occurring in the setup witch made them decoupled.

## How to run

Process of running application is straighforward.

### Step one

Clone the repo on desired location

### Step two

Go to the cloned folder and run `docker-compose-up` and wait for containers to be initialized.

Once is done, you should see this picture:

![img](https://i.imgur.com/KXHVSoh.png)

### Step three

There are few containers you can run for browser via URL provided:

- **Create feedback service**: [Swagger documentation](https://localhost:5001/api/docs/index.html)

- **Feedback analytics service**: [Swagger documentation](https://localhost:6001/api/docs/index.html)

- **RabbitMQ portal**: [UI portal](http://localhost:15672/) (credentials are **guest**/**guest**)

- **Seq portal**: [UI portal](http://localhost:5050/)

- **Jagger portal**: [UI portal](http://localhost:16686/)

## Technical decisions

In section *2.2. Feedback Submission Endpoint* for *customer_id* I made a custom extension method that returs random numbers from 0 to 5000 for sake of simplicity.
