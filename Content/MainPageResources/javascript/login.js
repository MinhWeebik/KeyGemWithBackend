const inputField = document.getElementById('1');
const label = inputField.nextElementSibling;
inputField.addEventListener('input', (e) => {
  if (e.target.value !== '') {
    label.parentNode.classList.add('has-value');
  } else {
    label.parentNode.classList.remove('has-value');
  }
});
const inputField2 = document.getElementById('2');
const label2 = inputField2.nextElementSibling;
inputField2.addEventListener('input', (e) => {
  if (e.target.value !== '') {
    label2.parentNode.classList.add('has-value2');
  } else {
    label2.parentNode.classList.remove('has-value2');
  }
});