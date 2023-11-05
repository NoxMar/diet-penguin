module.exports = {
    // use convetional commits config as a base
    extends: ['@commitlint/config-conventional'],
    'rules': {
        'header-max-length': [2, 'always', 70]
    },
};
