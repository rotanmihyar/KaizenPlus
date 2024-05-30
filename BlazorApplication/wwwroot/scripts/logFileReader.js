window.readLogFile = async function () {
    try {
        // Read the log file content using fetch API
        let response = await fetch('C://log.txt');
        if (response.ok) {
            return await response.text();
        } else {
            throw new Error(`Failed to read log file: ${response.statusText}`);
        }
    } catch (error) {
        throw new Error(`Error reading log file: ${error.message}`);
    }
}
