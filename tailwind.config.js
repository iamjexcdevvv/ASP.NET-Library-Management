/** @type {import('tailwindcss').Config} */
module.exports = {
    content: [
        "./Views/**/*.{html,cshtml}",
        "./wwwroot/**/*.html"
    ],
    theme: {
        extend: {
            backgroundImage: {
                'library': "url('/resources/library.jpg')"
            }
        },
    },
    plugins: [
        require('daisyui')
    ],
    daisyui: {
        themes: ["corporate"],
    }
}