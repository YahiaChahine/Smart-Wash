import requests


def send_machine_status(machine_id, status):
    url = "https://localhost:7114/api/machine/status"
    payload = {"MachineId": machine_id, "Status": status}

    headers = {"Content-Type": "application/json"}

    r = requests.utils.default_headers()

    response = requests.post(url, json=payload, headers=r, verify=False)

    if response.status_code == 200:
        print(f"Status update sent successfully")
    else:
        print(f"Failed to send status update: {response.status_code}, {response.text}")


def access_machine(machine_id, password):
    url = "https://localhost:7114/api/machine/access"
    payload = {"MachineId": machine_id, "Password": password}

    r = requests.utils.default_headers()

    response = requests.post(url, json=payload, headers=r, verify=False)

    if response.status_code == 200:
        print(f"\n\nAccess granted")
        return response.json()
    else:
        print(f"\n\nAccess denied: {response.status_code}, {response.text}")


if __name__ == "__main__":
    # # Example usage
    # machine_id = 12
    # status = "Washing"
    # send_machine_status(machine_id, status)

    # Ask the user for the machine ID and password
    machine_id = int(input("\nEnter machine ID: "))
    password = input("Enter password: ")

    # Access the machine
    response = access_machine(machine_id, password)

    # If access is granted, print the response
    if response:
        if response["isPaid"]:
            print(
                "\n\nAccess granted, you booked "
                + str(response["cycleNum"])
                + " cycle(s)"
            )
            print(response)

            send_machine_status(machine_id, "Washing")
        else:
            p = input(
                "\n\nPay by coin, enter the amount: " + str(response["amount"]) + " : "
            )
            if p == str(response["amount"]):
                print(
                    "\n\nAccess granted, you booked "
                    + str(response["cycleNum"])
                    + " cycles"
                )
                print(response)

                send_machine_status(machine_id, "Washing")
            else:
                print("\n\nPayment failed")


input("\n\nPress Enter to exit...")


class Booking:
    def __init__(self, booking_id, machine_id, start_time, end_time, cycle_num):
        self.booking_id = booking_id
        self.machine_id = machine_id
        self.start_time = start_time
        self.end_time = end_time
        self.cycle_num = cycle_num

    def __str__(self):
        return f"Booking ID: {self.booking_id}, Machine ID: {self.machine_id}, Start Time: {self.start_time}, End Time: {self.end_time}"

    def __repr__(self):
        return f"Booking ID: {self.booking_id}, Machine ID: {self.machine_id}, Start Time: {self.start_time}, End Time: {self.end_time}"
