export type Project = {
    id: number;
    name: string;
    status: string,
    deadline: string,
    totalMinutes: number
}

export type Entry = {
    id: number;
    description: string;
    minutes: number;
    projectId: number;
}