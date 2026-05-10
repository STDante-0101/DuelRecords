// Ambient star layer — drawn once when the page loads.
(function () {
    const ambience = document.getElementById("ambience");
    if (!ambience) return;
    const N = 80;
    const stars = [];
    for (let i = 0; i < N; i++) {
        const s = document.createElement("div");
        const sz = Math.random() * 2 + 0.5;
        Object.assign(s.style, {
            position: "absolute",
            width: sz + "px", height: sz + "px",
            borderRadius: "50%",
            background: `rgba(255,255,255,${0.2 + Math.random() * 0.5})`,
            left: (Math.random() * 100) + "%",
            top: (Math.random() * 100) + "%",
            boxShadow: `0 0 ${sz * 2}px rgba(167,139,250,0.4)`,
            opacity: Math.random(),
            transition: "opacity 3s ease",
        });
        ambience.appendChild(s);
        stars.push(s);
    }
    setInterval(() => {
        stars.forEach(s => { s.style.opacity = Math.random(); });
    }, 2500);
})();

// JS interop helpers callable from C#
window.duelRecords = {
    setTheme: (theme) => {
        document.documentElement.dataset.theme = theme;
        try { localStorage.setItem("duel-theme", theme); } catch { }
    },
    getTheme: () => {
        try { return localStorage.getItem("duel-theme") || "dark"; } catch { return "dark"; }
    },
    setCssVar: (name, value) => {
        document.documentElement.style.setProperty(name, value);
    },
    confirm: (message) => window.confirm(message)
};
