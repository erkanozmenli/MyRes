const fs = require("fs");
const { execSync } = require("child_process");

const config = JSON.parse(
    fs.readFileSync(".github/services.json", "utf8")
);

const event = JSON.parse(
    fs.readFileSync(process.env.GITHUB_EVENT_PATH, "utf8")
);

const services = config.filter(item => item.type === "service" || item.type === "frontend");
const sharedProjects = config.filter(item => item.type === "shared");

const before = event.before;
const after = process.env.GITHUB_SHA;

const changedFiles = execSync(`git diff --name-only ${before} ${after}`)
    .toString()
    .trim()
    .split("\n")
    .filter(Boolean);

const hasChangesUnder = directory =>
    changedFiles.some(file => file.startsWith(`${directory}/`));

const sharedChanged = sharedProjects.some(project =>
    hasChangesUnder(project.source)
);

let selectedServices = services.filter(service =>
    hasChangesUnder(service.source)
);

if (sharedChanged) {
    selectedServices = services;
}

const matrix = {
    include: selectedServices
};

process.stdout.write(`matrix=${JSON.stringify(matrix)}\n`);
process.stdout.write(`hasChanges=${selectedServices.length > 0}\n`);