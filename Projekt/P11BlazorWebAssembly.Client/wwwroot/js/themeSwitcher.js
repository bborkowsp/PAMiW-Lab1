window.themeSwitcher = {
    setTheme: function (theme) {
        document.getElementById('theme-stylesheet').href = `css/${theme}-theme.css`;
    }
};