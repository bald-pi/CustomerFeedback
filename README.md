<h1 align="center"> Customer feedback </h1>

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
    - [Create feedback service](#create-feedback-service)
    - [Feedback analytics service](#feedback-analytics-service)
    - [RabbitMQ portal](#rabbitmq-portal)
    - [Seq](#seq)

<!-- END doctoc generated TOC please keep comment here to allow auto update -->

## Overview

View repository and user information, control your notifications and even manage your issues and pull requests. Built with React Native, GitPoint is one of the most feature-rich unofficial GitHub clients that is 100% free.

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
<br>
- **Feedback analytics service**: [Swagger documentation](https://localhost:6001/api/docs/index.html)
<br>
- **RabbitMQ portal**: [UI portal](http://localhost:15672/) (credentials are **guest**/**guest**)
<br>
- **Seq portal**: [UI portal](http://localhost:5050/)
