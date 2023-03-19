const BASE_URL = "http://localhost:3001/api";

export async function getAll(search: string) {
    const response = await fetch(`${BASE_URL}/projects?search=${search}`);
    return response.json();
}

export async function addEntry(description: string, projectId: number, minutes: number) {
    var response = await fetch(`${BASE_URL}/projects/addEntry`, {
        method: 'POST',
        mode: 'cors',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json',
        },
        body: JSON.stringify({
            description,
            projectId,
            minutes
        })
    });
    return response.json();
}

export async function get(id: number) {
    const response = await fetch(`${BASE_URL}/projects/${id}`);
    return response.json();
}