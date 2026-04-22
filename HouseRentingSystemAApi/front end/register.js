async function registerUser() {
    const payload = {
        username: "john2",
        email: "john2@gmail.com",
        password: "123456"
    };

    try {
        const response = await fetch("http://localhost:5044/register", {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify(payload)
        });

        const data = await response.json();

        if (!response.ok) {
            console.error("Register failed:", data);
            return;
        }

        console.log("Register success:", data);
    } catch (error) {
        console.error("Network error:", error);
    }
}

registerUser();
