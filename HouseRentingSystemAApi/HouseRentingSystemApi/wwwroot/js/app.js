const tokenKey = "hrs_jwt_token";
const housesGrid = document.getElementById("housesGrid");
const messageBox = document.getElementById("messageBox");
const authState = document.getElementById("authState");
const createForm = document.getElementById("createHouseForm");
const createAuthNote = document.getElementById("createAuthNote");
const guestActions = document.getElementById("guestActions");
const userActions = document.getElementById("userActions");
const authModalTitle = document.getElementById("authModalTitle");
const loginForm = document.getElementById("loginForm");
const registerForm = document.getElementById("registerForm");
const openLoginBtn = document.getElementById("openLoginBtn");
const openRegisterBtn = document.getElementById("openRegisterBtn");
const createSection = document.getElementById("create");
const addHouseNavItem = document.getElementById("addHouseNavItem");
const addHouseLink = document.getElementById("addHouseLink");
const authModal = new bootstrap.Modal(document.getElementById("authModal"));

document.getElementById("refreshBtn").addEventListener("click", loadHouses);
document.getElementById("logoutBtn").addEventListener("click", logout);
loginForm.addEventListener("submit", login);
registerForm.addEventListener("submit", register);
document.getElementById("createHouseForm").addEventListener("submit", createHouse);
openLoginBtn.addEventListener("click", () => openAuthModal("login"));
openRegisterBtn.addEventListener("click", () => openAuthModal("register"));
if (addHouseLink) {
    addHouseLink.addEventListener("click", openCreateSection);
}

updateAuthState();
loadHouses();

function showMessage(text, type = "info") {
    messageBox.className = `alert alert-${type} mt-3`;
    messageBox.textContent = text;
    messageBox.classList.remove("d-none");
}

function hideMessage() {
    messageBox.classList.add("d-none");
}

function getToken() {
    return localStorage.getItem(tokenKey);
}

function updateAuthState() {
    const isLogged = !!getToken();
    authState.textContent = "Logged In";
    authState.className = "badge text-bg-success";

    if (isLogged) {
        guestActions.classList.add("d-none");
        userActions.classList.remove("d-none");
        addHouseNavItem.classList.remove("d-none");
        createSection.classList.remove("d-none");
    } else {
        guestActions.classList.remove("d-none");
        userActions.classList.add("d-none");
        createSection.classList.add("d-none");
        addHouseNavItem.classList.add("d-none");
    }

    Array.from(createForm.elements).forEach(el => {
        if (el.tagName === "BUTTON" || el.tagName === "INPUT" || el.tagName === "TEXTAREA" || el.tagName === "SELECT") {
            el.disabled = !isLogged;
        }
    });

    if (isLogged) {
        createAuthNote.classList.add("d-none");
    } else {
        createAuthNote.classList.remove("d-none");
    }
}

function logout() {
    localStorage.removeItem(tokenKey);
    updateAuthState();
    showMessage("Logged out.", "secondary");
}

function openAuthModal(mode) {
    if (mode === "register") {
        authModalTitle.textContent = "Register";
        loginForm.classList.add("d-none");
        registerForm.classList.remove("d-none");
    } else {
        authModalTitle.textContent = "Login";
        registerForm.classList.add("d-none");
        loginForm.classList.remove("d-none");
    }

    authModal.show();
}

function openCreateSection(e) {
    e.preventDefault();

    if (!getToken()) {
        showMessage("Трябва да си логнат, за да добавяш къщи.", "warning");
        openAuthModal("login");
        return;
    }

    createSection.classList.remove("d-none");
    createSection.scrollIntoView({ behavior: "smooth", block: "start" });
}

function openCreateSectionFromNav(e) {
    openCreateSection(e);
}

async function login(e) {
    e.preventDefault();
    hideMessage();

    const payload = {
        email: document.getElementById("loginEmail").value.trim(),
        password: document.getElementById("loginPassword").value.trim()
    };

    const response = await fetch("/login", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(payload)
    });

    if (!response.ok) {
        showMessage("Invalid login credentials.", "danger");
        return;
    }

    const loginResult = await response.json();
    localStorage.setItem(tokenKey, loginResult.token);
    updateAuthState();
    authModal.hide();
    loginForm.reset();
    showMessage("Login successful.", "success");
}

async function register(e) {
    e.preventDefault();
    hideMessage();

    const payload = {
        username: document.getElementById("registerUsername").value.trim(),
        email: document.getElementById("registerEmail").value.trim(),
        password: document.getElementById("registerPassword").value.trim()
    };

    const response = await fetch("/register", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(payload)
    });

    if (!response.ok) {
        showMessage("Registration failed.", "danger");
        return;
    }

    showMessage("Registration successful. You can now login.", "success");
    registerForm.reset();
    openAuthModal("login");
}

async function loadHouses() {
    housesGrid.innerHTML = `<div class="col-12"><div class="alert alert-light">Loading houses...</div></div>`;
    try {
        const response = await fetch("/api/House/All");
        if (!response.ok) {
            housesGrid.innerHTML = `<div class="col-12"><div class="alert alert-danger">Could not load houses.</div></div>`;
            return;
        }

        const houses = await response.json();
        if (!houses.length) {
            housesGrid.innerHTML = `<div class="col-12"><div class="alert alert-secondary">No houses available.</div></div>`;
            return;
        }

        housesGrid.innerHTML = houses.map(h => `
            <div class="col-12 col-sm-6 col-lg-4">
                <div class="card house-card h-100 shadow-sm">
                    <img src="${h.imageUrl}" class="card-img-top" alt="${escapeHtml(h.title)}">
                    <div class="card-body d-flex flex-column">
                        <h5 class="card-title">${escapeHtml(h.title)}</h5>
                        <p class="card-text mb-1">${escapeHtml(h.address)}</p>
                        <p class="small text-muted mb-3">$${Number(h.pricePerMonth || 0).toFixed(2)}/month</p>
                        <button class="btn btn-outline-primary mt-auto" onclick="showDetails(${h.id})">Details</button>
                    </div>
                </div>
            </div>
        `).join("");
    } catch {
        housesGrid.innerHTML = `<div class="col-12"><div class="alert alert-danger">Error while loading houses.</div></div>`;
    }
}

async function showDetails(id) {
    const response = await fetch(`/api/House/${id}`);
    if (!response.ok) {
        showMessage("House not found.", "warning");
        return;
    }

    const h = await response.json();
    const detailsBody = document.getElementById("detailsBody");
    detailsBody.innerHTML = `
        <img src="${h.imageUrl}" alt="${escapeHtml(h.title)}">
        <h4>${escapeHtml(h.title)}</h4>
        <p><strong>Address:</strong> ${escapeHtml(h.address)}</p>
        <p><strong>Description:</strong> ${escapeHtml(h.description || "No description")}</p>
        <p><strong>Price:</strong> $${Number(h.pricePerMonth || 0).toFixed(2)} / month</p>
    `;

    const modal = new bootstrap.Modal(document.getElementById("detailsModal"));
    modal.show();
}

async function createHouse(e) {
    e.preventDefault();
    hideMessage();

    const token = getToken();
    if (!token) {
        showMessage("Login first to create a house.", "warning");
        return;
    }

    const payload = {
        title: document.getElementById("houseTitle").value.trim(),
        address: document.getElementById("houseAddress").value.trim(),
        imageUrl: document.getElementById("houseImageUrl").value.trim(),
        description: document.getElementById("houseDescription").value.trim(),
        pricePerMonth: Number(document.getElementById("housePrice").value),
        category: Number(document.getElementById("houseCategory").value)
    };

    const response = await fetch("/api/House", {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
            "Authorization": `Bearer ${token}`
        },
        body: JSON.stringify(payload)
    });

    if (!response.ok) {
        showMessage("Create failed. Check data and token.", "danger");
        return;
    }

    showMessage("House created successfully.", "success");
    e.target.reset();
    loadHouses();
}

function escapeHtml(text) {
    const div = document.createElement("div");
    div.textContent = text ?? "";
    return div.innerHTML;
}

window.showDetails = showDetails;
window.openCreateSectionFromNav = openCreateSectionFromNav;
