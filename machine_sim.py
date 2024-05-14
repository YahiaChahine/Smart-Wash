import requests


def send_machine_status(machine_id, status):
    url = "https://localhost:7114/api/machine/status"  # Adjust the port if your app listens on a different port
    payload = {"MachineId": machine_id, "Status": status}

    headers = {"Content-Type": "application/json"}

    r = requests.utils.default_headers()

    response = requests.post(url, json=payload, headers=r, verify=False)

    if response.status_code == 200:
        print(f"Status update sent successfully")
    else:
        print(f"Failed to send status update: {response.status_code}, {response.text}")


if __name__ == "__main__":
    # Example usage
    machine_id = 12
    status = "Washing"
    send_machine_status(machine_id, status)
