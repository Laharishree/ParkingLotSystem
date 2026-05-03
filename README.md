# ParkingLotSystem

##Problem statement
You are tasked with designing a parking system for a multi-level parking lot. The parking lot can accommodate different types of vehicles, including motorcycles, cars, and buses. The system should efficiently manage the parking process, including vehicle entry, parking slot assignment, and exit.

Requirements
Parking Lot Structure

The parking lot has multiple levels, each with a fixed number of parking slots.
Slots are categorized based on vehicle types:
Motorcycle Slot: Can accommodate motorcycles only.
Car Slot: Can accommodate cars and motorcycles.
Bus Slot: Can accommodate buses, cars, and motorcycles.
Vehicle Types

Motorcycle
Car
Bus
Parking Process

A vehicle enters the parking lot and is assigned an available parking slot based on its type.
If no suitable slot is available, the vehicle cannot enter the parking lot.
The system should minimize the time taken to find a parking slot.
Exit Process

When a vehicle exits, the system should free up the parking slot.
The system should calculate the parking fee based on the vehicle type and the duration of the parking.
Parking Fee

Motorcycle: $1 per hour.
Car: $2 per hour.
Bus: $5 per hour.
Admin Features

The admin can view the status of the parking lot (number of free/occupied slots by type).
The admin can add or remove levels or slots.
Commands
Add Parking Level

ADD_LEVEL <level_id> <num_motorcycle_slots> <num_car_slots> <num_bus_slots>
Example: ADD_LEVEL 1 10 20 5
Output: Level 1 added with 10 motorcycle slots, 20 car slots, 5 bus slots.
Park Vehicle

PARK_VEHICLE <vehicle_type> <license_plate_number>
Example: PARK_VEHICLE CAR KA-01-HH-1234
Output: Car with license plate KA-01-HH-1234 parked at level 1, slot 15.
Exit Vehicle

EXIT_VEHICLE <license_plate_number>
Example: EXIT_VEHICLE KA-01-HH-1234
Output: Car with license plate KA-01-HH-1234 exited. Fee: $10.
View Parking Lot Status

VIEW_STATUS
Output: Level 1: 5/10 motorcycle slots, 10/20 car slots, 2/5 bus slots available.
Admin: Add/Remove Slots or Levels

ADD_SLOTS <level_id> <slot_type> <number_of_slots>
REMOVE_SLOTS <level_id> <slot_type> <number_of_slots>
Expectations
Class Design

Define classes for ParkingLot, ParkingLevel, ParkingSlot, Vehicle, Admin, etc.
Each class should have clear responsibilities, such as managing slots, handling vehicle entry/exit, calculating fees, etc.
Data Structures

Use efficient data structures to track available slots, occupied slots, and vehicle information.
Exception Handling

Handle cases where the parking lot is full, invalid commands are issued, or vehicle information is incorrect.
Extensibility

The system should be designed to easily accommodate future changes, such as adding new vehicle types or modifying parking fees.
Edge Cases

Handle scenarios where multiple vehicles try to park at the same time, a vehicle tries to exit without entering, etc.
Bonus Requirements
Optimized Slot Allocation

Design the system to optimize the parking slot assignment to minimize walking distance for the driver.
Reservation System

Implement a feature where a vehicle can reserve a parking slot in advance.
Real-Time Status Monitoring

Allow real-time monitoring of the parking lot’s status, including live updates of available slots.
Interview Guidelines
Clarify Requirements: Encourage the candidate to ask clarifying questions about the parking system.
Focus on Design: Evaluate the candidate’s ability to break down the problem, identify key components, and design a modular, extensible solution.
Code Quality: Assess the candidate’s ability to write clean, maintainable code with proper naming conventions and comments.
Testing and Edge Cases: Check if the candidate considers edge cases and writes test scenarios to validate their design.
refer : http://medium.com/@mehar.chand.cloud/low-level-design-interview-question-parking-system-a041bd1973d2