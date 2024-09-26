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
- **OpenTelemetry** for distributed tracing on top of **Jaeger**.
- **Serilog** for structured logging
- **Seq** for structured logging centralization
- **Fluent validation** for building strongly-typed validation rules
- **Flunt results** for Result pattern
- **Asp versioning** for API's versioning

## Project structure

![alt text](https://i.imgur.com/4BHefsZ.png)

In this project I've used [vertical slice architecture](https://jimmybogard.com/vertical-slice-architecture/) + combined with [feature folder structure] (http://www.kamilgrzybek.com/design/feature-folders/).
I treat each request as a distinct use case or slice, encapsulating and grouping all related concerns together.
This approach minimizes coupling between slices and maximizes coupling within a slice.
Each slice can independently decide how to best fulfill the request.
When a new feature is added, there's only new code to write.
Side effects are minimal, and shared code is rarely changed.

I used RabbitMQ as a message broker for asynchronous communication between microservices, employing the eventual consistency mechanism.
Each microservice utilizes MassTransit for broker communications.
Microservices are event-driven, enabling them to publish and/or subscribe to any events occurring in the system, making them decoupled.

## How to run

Running the application is straightforward.

### Step one

Clone the repository to your desired location

### Step two

Navigate to the cloned folder, then execute `docker-compose-up` and wait for the containers to start.

When finished, you will see this picture:

![img](https://i.imgur.com/KXHVSoh.png)

### Step three

You can access the containers via the provided URLs:

- **Create feedback service**: [Swagger documentation](https://localhost:5001/api/docs/index.html)

- **Feedback analytics service**: [Swagger documentation](https://localhost:6001/api/docs/index.html)

- **RabbitMQ portal**: [UI portal](http://localhost:15672/) (credentials are **guest**/**guest**)

- **Seq portal**: [UI portal](http://localhost:5050/)

- **Jagger portal**: [UI portal](http://localhost:16686/)

## Technical decisions

In section *2.2. Feedback Submission Endpoint* for *customer_id* I implemented a custom extension method that generates random numbers between 0 and 5000 for simplicity.
